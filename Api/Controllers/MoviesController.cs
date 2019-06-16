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

        public MoviesController(IGetMoviesCommand command, IGetMovieCommand oneCommand, IAddMovieCommand addMovieCommand, IDeleteMovieCommand deleteMovieCommand, IUpdateMovieCommand updateCommand)
        {
            _command = command;
            _oneCommand = oneCommand;
            _addMovieCommand = addMovieCommand;
            _deleteMovieCommand = deleteMovieCommand;
            _updateCommand = updateCommand;
        }





        /// <summary>
        /// Returns all movies that match provided query
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///  
        ///  GET api/movies
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

        [HttpGet]
        public ActionResult<IEnumerable<MovieDto>> Get([FromQuery]MovieSearch search)
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
        ///  GET api/movies/3
        ///  {
        ///      
        ///  }
        /// 
        /// </remarks>
        /// <param name="id"></param>  
        // GET: api/Movies/5
     
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
        ///  POST api/movies
        ///  {
        ///      "title" = "Godfather2",
        ///      "description" = 1997,
        ///      "availableCount" = true,
        ///      "count" = 3,
        ///      "year" = 2012,
        ///      "directorId" = 1,
        ///      "selectedGenres" : [
        ///             1,2 
        ///     ]
        ///  }
        /// 
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        // POST: api/Movies
        [HttpPost]
        public ActionResult Post([FromBody] MovieDto dto)
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
        /// Update movie with provided id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///  
        ///  PUT api/movies/3
        ///  {
        ///      "title" = "Godfather2",
        ///      "description" = 1997,
        ///      "availableCount" = true,
        ///      "count" = 3,
        ///      "year" = 2012,
        ///      "directorId" = 1,
        ///      "selectedGenres" : [
        ///             1,2 
        ///     ]
        ///  }
        /// 
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        // PUT: api/Movies/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] MovieDto dto)
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
        /// Delete movie with provided id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///  
        ///  DELETE api/movies/4
        /// 
        /// </remarks>
        /// <param name="id"></param>  
        // DELETE: api/movies/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
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
