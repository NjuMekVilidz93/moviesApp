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
    public class UsersController : ControllerBase
    {
        private readonly IGetUsersCommand _getCommand;
        private readonly IUpdateUserCommand _updateCommand;
        private readonly IDeleteUserCommand _deleteCommand;
        private readonly IAddUserCommand _addCommand;

        public UsersController(IGetUsersCommand getCommand, IUpdateUserCommand updateCommand, IDeleteUserCommand deleteCommand, IAddUserCommand addCommand)
        {
            _getCommand = getCommand;
            _updateCommand = updateCommand;
            _deleteCommand = deleteCommand;
            _addCommand = addCommand;
        }
        /// <summary>
        /// Returns all users that match provided query
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///  
        ///  GET api/users
        ///  {
        ///      "firstName" = "Nikola",
        ///      "lastName" = "Vulovic",
        ///      "username" = "vule"
        ///  }
        /// 
        /// </remarks>
        // GET: api/Users
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> Get([FromQuery] UserSearch search)
        {
            try
            {
                var result = _getCommand.Execute(search);
                return Ok(result);
            }
            catch (Exception)
            {

                return StatusCode(500,"An error has ocured.");
            }
        }
      
        // GET: api/Users/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        /// <summary>
        /// A newly created user
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///  
        ///  POST api/users
        ///  {
        ///      "firstName" = "Marko",
        ///      "lastName" = "Markovic",
        ///      "username" = "mare",
        ///      "password" = "markic123",
        ///      "roleId" = 2
        ///  }
        /// 
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        // POST: api/Users
        [HttpPost]
        public ActionResult Post([FromBody] UserDto dto)
        {
            try
            {
                _addCommand.Execute(dto);
                return StatusCode(201, "Success.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error has occured.");
            }
        }
        /// <summary>
        /// Update user
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///  
        ///  PUT api/users/3
        ///  {
        ///      "firstName" = "Pera",
        ///      "lastName" = "Peric",
        ///      "username" = "pera",
        ///      "password" = "pera123",
        ///      "roleId" = 1
        ///  }
        /// 
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="dto"></param>  
        // PUT: api/Users/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] UserDto dto)
        {
            dto.Id = id;

            try
            {
                _updateCommand.Execute(dto);
                return StatusCode(204, "Success.");
            }
            catch (Exception e)
            {
                return StatusCode(409, e.Message);
            }
        }
        /// <summary>
        /// Delete user
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///  
        ///  DELETE api/users/4
        /// 
        /// </remarks>
        /// <param name="id"></param>  
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _deleteCommand.Execute(id);
                return StatusCode(204, "Success.");
            }
            catch (EntityNotFoundException e)
            {

                return StatusCode(40, e.Message);
            }
        }
    }
}
