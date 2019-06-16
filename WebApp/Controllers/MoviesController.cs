using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using Application.Searches;
using EfDataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IGetGenresCommand _getCommand;
        private readonly IGetMoviesCommand _getMovies;
        private readonly IGetAllMoviesCommand _getAll;
        private readonly IGetMovieCommand _getOne;
        private readonly IEditMovieCommand _editMovie;
        private readonly IDeleteMovieCommand _deleteCommand;
        private readonly ICreateMovieCommand _create;
        private readonly IGetOneMovieCommand _getOneCommand;

        public MoviesController(IGetGenresCommand getCommand, IGetMoviesCommand getMovies, IGetAllMoviesCommand getAll, IGetMovieCommand getOne, IEditMovieCommand editMovie, IDeleteMovieCommand deleteCommand, ICreateMovieCommand create, IGetOneMovieCommand getOneCommand)
        {
            _getCommand = getCommand;
            _getMovies = getMovies;
            _getAll = getAll;
            _getOne = getOne;
            _editMovie = editMovie;
            _deleteCommand = deleteCommand;
            _create = create;
            _getOneCommand = getOneCommand;
        }


        // GET: Movies
        public ActionResult Index(MovieSearch search)
        {
            try
            {
                var result = _getAll.Execute(search);
                return View(result);
            }
            catch (Exception)
            {
                TempData["error"] = "Error";
                return View();
            }
                
        }

        // GET: Movies/Details/5
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

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                _create.Execute(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistsException)
            {
                TempData["error"] = "Movie already exist.";
            } catch (Exception)
            {
                TempData["error"] = "An error has occured.";
            }
            return View();
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var dto = _getOneCommand.Execute(id);
                return View(dto);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }

        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MovieDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                _editMovie.Execute(dto);
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

        //GET: Movies/Delete/5
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


        public IActionResult Catalog([FromQuery]MovieSearch search)
        {
            var vm = new CatalogViewModel();
            
           vm.Genres = _getCommand.Execute(new Application.Searches.GenreSearch
            {
                OnlyActive = true
            });

           vm.Movies = _getMovies.Execute(search);

            return View(vm);

        }
    }
}