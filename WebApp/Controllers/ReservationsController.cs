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

namespace WebApp.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IGetReservationsCommand _getCommand;
        private readonly IGetReservationCommand _getOneCommand;
        private readonly IDeleteReservationCommand _deleteCommand;
        private readonly ICreateReservationCommand _createCommand;
        private readonly IGetOneReservationCommand _getReservation;
        private readonly IEditReservationCommand _editCommand;
        private readonly IMovieUserCommand _userMovie;

        public ReservationsController(IGetReservationsCommand getCommand, IGetReservationCommand getOneCommand, IDeleteReservationCommand deleteCommand, ICreateReservationCommand createCommand, IGetOneReservationCommand getReservation, IEditReservationCommand editCommand, IMovieUserCommand userMovie)
        {
            _getCommand = getCommand;
            _getOneCommand = getOneCommand;
            _deleteCommand = deleteCommand;
            _createCommand = createCommand;
            _getReservation = getReservation;
            _editCommand = editCommand;
            _userMovie = userMovie;
        }




        // GET: Reservations
        public ActionResult Index(ReservationSearch search)
        {
            try
            {
                var reservation = _getCommand.Execute(search);
                return View(reservation);
            }
            catch (Exception)
            {
                TempData["error"] = "Error";
                return View();
            }

        }

        // GET: Reservations/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var result = _getOneCommand.Execute(id);
                return View(result);

            }
            catch (Exception)
            {
                TempData["error"] = "Error";
                return View();
            }
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reservations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReservationDto dto)
        {

            try
            {
                _createCommand.Execute(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistsException)
            {
                TempData["error"] = "Movie already exist.";
            }
            catch (Exception)
            {
                TempData["error"] = "An error has occured.";
            }
            return View();
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var dto = _getReservation.Execute(id);
                return View(dto);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }

        }

        // POST: Reservations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ReservationDto dto)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(dto);
            //}
            try
            {
                _editCommand.Execute(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityNotFoundException)
            {
                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistsException)
            {
                TempData["error"] = "Movie already exist.";
                return View(dto);
            }
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                _deleteCommand.Execute(id);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityNotFoundException)
            {
                return View();
            }
        }

        public ActionResult MovieUser(int id)
        {
            try
            {
                _userMovie.Execute(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }

        }

    }
}