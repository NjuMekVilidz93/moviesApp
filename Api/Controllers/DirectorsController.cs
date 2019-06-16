using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using Application.Searches;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private readonly IAddDirectorCommand _addCommand;
        private readonly IGetDirectorsCommand _getCommand;
        private readonly IGetDirectorCommand _getOneCommand;
        private readonly IUpdateDirectorCommand _updateCommand;
        private readonly IDeleteDirectorCommand _deleteCommand;

        public DirectorsController(IAddDirectorCommand addCommand, IGetDirectorsCommand getCommand, IGetDirectorCommand getOneCommand, IUpdateDirectorCommand updateCommand, IDeleteDirectorCommand deleteCommand)
        {
            _addCommand = addCommand;
            _getCommand = getCommand;
            _getOneCommand = getOneCommand;
            _updateCommand = updateCommand;
            _deleteCommand = deleteCommand;
        }

        /// <summary>
        /// Returns all directors that match provided query
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///  
        ///  GET /directors
        ///  {
        ///      "name" = "Tarantino"
        ///  }
        /// 
        /// </remarks>
        // GET: api/Directors
        [HttpGet]
        public ActionResult<IEnumerable<DirectorDto>> Get([FromQuery]DirectorSearch search)
        {
            var result = _getCommand.Execute(search);
            return Ok(result);
        }
        /// <summary>
        /// Returns one director
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///  
        ///  GET /directors/2
        /// </remarks>
        // GET: api/Directors/5
        [HttpGet("{id}")]
        public ActionResult<DirectorDto> Get(int id)
        {
            try
            {
                var director = _getOneCommand.Execute(id);
                return Ok(director);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        /// <summary>
        /// A newly created director
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///  
        ///  POST /directors
        ///  {
        ///       "id" = "3"
        ///      "name" = "Fredy"
        ///  }
        /// 
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        // POST: api/Directors
        [HttpPost]
        public ActionResult<DirectorDto> Post([FromBody] DirectorDto dto)
        {
            try
            {
                _addCommand.Execute(dto);
                return StatusCode(201,"Success.");
            }
            catch (Exception)
            {
                return StatusCode(500,"An error has occured.");
            }

        }
        /// <summary>
        /// Update director 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///  
        ///  PUT /directors/3
        ///  {
        ///
        ///      "name" = "Tarantino"
        ///  }
        /// 
        /// </remarks>
        // PUT: api/Directors/5
        [HttpPut("{id}")]
        public ActionResult<DirectorDto> Put(int id, [FromBody] DirectorDto dto)
        {
            dto.Id = id;
            try
            {
                _updateCommand.Execute(dto);
                return StatusCode(204,"Success.");
            }
            catch (Exception e)
            {
                return StatusCode(409,e.Message);
            }
        }
        /// <summary>
        /// Delete director
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///  
        ///  DELETE /directors/2
        ///  {
        ///      "id" = 2
        ///  }
        /// 
        /// </remarks>
        // DELETE: api/Directors/5
        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            try
            {
                _deleteCommand.Execute(id);
                return StatusCode(204,"Success.");
            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(404,e.Message);
            }
        }
    }
}
