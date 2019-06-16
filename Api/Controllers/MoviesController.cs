using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Helpers;
using Application;
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
    public class MoviesController : ControllerBase
    {
        private readonly IGetMoviesCommand _command;
        private readonly IGetMovieCommand _oneCommand;
        private readonly IAddMovieCommand _addMovieCommand;
        private readonly IDeleteMovieCommand _deleteMovieCommand;
        private readonly IUpdateMovieCommand _updateCommand;
        private readonly LoggedUser _user;

        public MoviesController(IGetMoviesCommand command, IGetMovieCommand oneCommand, IAddMovieCommand addMovieCommand, IDeleteMovieCommand deleteMovieCommand, IUpdateMovieCommand updateCommand, LoggedUser user)
        {
            _command = command;
            _oneCommand = oneCommand;
            _addMovieCommand = addMovieCommand;
            _deleteMovieCommand = deleteMovieCommand;
            _updateCommand = updateCommand;
            _user = user;
        }


        /// <summary>
        /// Returns all movies that match provided query
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///  
        ///  GET /movies
        ///  {
        ///      "movieName" = "Godfather",
        ///      "movieYear" = 1997,
        ///      "isAvailable" = true,
        ///      "genreId" = 3,
        ///      "perPage" = 4,
        ///      "pageNumber" = 1
        ///  }
        /// 
        /// </remarks>
        // GET: api/Movies
        [LoggedIn("Admin")]
        [HttpGet]
        public ActionResult<MovieDto> Get([FromQuery]MovieSearch search)
        {
            var result = _command.Execute(search);
            return Ok(result);
        }
        /// <summary>
        /// Return one movie
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///  
        ///  GET /movies/3
        ///  {
        ///      "movieName" = "Godfather",
        ///      "movieYear" = 1997,
        ///      "isAvailable" = true,
        ///      "genreId" = 3,
        ///      "perPage" = 4,
        ///      "pageNumber" = 1
        ///  }
        /// 
        /// </remarks>
        /// <param name="id"></param>  
        // GET: api/Movies/5
        [LoggedIn]
        [HttpGet("{id}")]
        public ActionResult<MovieDto> Get(int id)
        {
            try
            {
                var movieDto = _oneCommand.Execute(id);
                return Ok(movieDto);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        /// <summary>
        /// A newly created movie
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///  
        ///  POST /movies
        ///  {
        ///      "title" = "Godfather2",
        ///      "description" = 1997,
        ///      "availableCount" = true,
        ///      "count" = 3,
        ///      "year" = 4,
        ///      "directorId" = 1,
        ///      "createdAt" = DateTime.Now
        ///  }
        /// 
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        // POST: api/Movies
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public ActionResult<MovieDto> Post([FromBody] MovieDto dto)
        {
            try
            {
                _addMovieCommand.Execute(dto);
                return StatusCode(201,"Success.");
            }
            catch (Exception)
            {
                return StatusCode(500,"An error has occured.");
            }
        }
        /// <summary>
        /// Update movie
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///  
        ///  PUT /movies/3
        ///  {
        ///      "title" = "Godfather2",
        ///      "description" = 1997,
        ///      "availableCount" = true,
        ///      "count" = 3,
        ///      "year" = 4,
        ///      "directorId" = 1,
        ///      "createdAt" = DateTime.Now
        ///  }
        /// 
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="dto"></param>  
        // PUT: api/Movies/5
        [HttpPut("{id}")]
        public ActionResult<MovieDto> Put(int id, [FromBody] MovieDto dto)
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
        /// Delete movie
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///  
        ///  DELETE /movies/4
        ///  {
        ///      "id" = 2
        ///  }
        /// 
        /// </remarks>
        // DELETE: api/movies/5
        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
         
            try
            {
                _deleteMovieCommand.Execute(id);
                return StatusCode(204,"Success.");
            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(404,e.Message);
            }
        }
    }
}
