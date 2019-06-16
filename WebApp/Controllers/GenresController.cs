using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Application.Searches;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class GenresController : Controller
    {
        private readonly IGetGenresCommand _getCommand;

        public GenresController(IGetGenresCommand getCommand)
        {
            _getCommand = getCommand;
        }

        // GET: Genres
        public ActionResult Index(GenreSearch search)
        {
            try
            {
                var result = _getCommand.Execute(search);
                return View(result);
            }
            catch (Exception)
            {
                TempData["error"] = "Error";
                return View();
            }
            
        }

        // GET: Genres/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Genres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Genres/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Genres/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Genres/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Genres/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}