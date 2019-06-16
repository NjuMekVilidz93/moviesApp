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
    public class ReservationsController : ControllerBase
    {

        private readonly IAddReservationCommand _addCommand;
        private readonly IGetReservationsCommand _getCommand;
        private readonly IGetReservationCommand _getOneCommand;
        private readonly IUpdateReservationCommand _updateCommand;
        private readonly IDeleteReservationCommand _deleteCommand;

        public ReservationsController(IAddReservationCommand addCommand, IGetReservationsCommand getCommand, IGetReservationCommand getOneCommand, IUpdateReservationCommand updateCommand, IDeleteReservationCommand deleteCommand)
        {
            _addCommand = addCommand;
            _getCommand = getCommand;
            _getOneCommand = getOneCommand;
            _updateCommand = updateCommand;
            _deleteCommand = deleteCommand;
        }
        /// <summary>
        /// Returns all reservations that match provided query
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///  
        ///  GET /reservations
        ///  {
        ///      "username" = "vule"
        ///  }
        /// 
        /// </remarks>
        // GET: api/Reservations
        [HttpGet]
        public ActionResult<ReservationDto> Get([FromQuery]ReservationSearch search)
        {
            try
            {
                var result = _getCommand.Execute(search);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error has occured");
            }
        }
        /// <summary>
        /// Return one reservation
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///  
        ///  GET /reservations/1
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
        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public ActionResult<ReservationDto> Get(int id)
        {
            try
            {
                var reservationDto = _getOneCommand.Execute(id);
                return Ok(reservationDto);

            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message); 
            }
        }
        /// <summary>
        /// A newly created reservation
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///  
        ///  POST /reservations
        ///  {
        ///    "createdAt" : "2018-04-02",
        ///    "userId" : 2,
        ///    "movieId" : 2
        ///  }
        /// 
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        // POST: api/Reservations
        [HttpPost]
        public ActionResult<ReservationDto> Post([FromBody] ReservationDto dto)
        {
            try
            {
                _addCommand.Execute(dto);
                return StatusCode(201,"Success");

            }
            catch (Exception)
            {

                return StatusCode(500,"An error has occured,please try later");
            }
        }
        /// <summary>
        /// Update reservation
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///  
        ///  PUT /reservations/3
        ///  {
        ///     "createdAt" : "2018-04-02",
        ///    "userId" : 1,
        ///    "movieId" : 1
        ///  }
        /// 
        /// </remarks>
        /// <param name="id"></param>  
        // PUT: api/Reservations/5
        [HttpPut("{id}")]
        public ActionResult<ReservationDto> Put(int id, [FromBody] ReservationDto dto)
        {
            try
            {
                dto.Id = id;
                _updateCommand.Execute(dto);
                return StatusCode(204);
            }
            catch (Exception e)
            {
                return StatusCode(409, e.Message);
            }
        }
        /// <summary>
        /// Delete reservation
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///  
        ///  DELETE /reservation/2
        ///  {
        ///      "id" = 2
        ///  }
        /// 
        /// </remarks>
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            try
            {
                _deleteCommand.Execute(id);
                return StatusCode(204, "Success.");
            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(404, e.Message);
            }
        }
    }
}
