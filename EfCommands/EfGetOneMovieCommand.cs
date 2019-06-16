using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfGetOneMovieCommand : EfBaseCommand, IGetOneMovieCommand
    {
        public EfGetOneMovieCommand(moviesContext context) : base(context)
        {
        }

        public MovieDto Execute(int request)
        {
  

            var movie = _context.Movies
               .Where(m => m.Id == request)
               .Include(m => m.Director)
               .ThenInclude(d => d.Name)
               .Include(m => m.MovieGenres)
               .ThenInclude(mg => mg.Genre)
               .Select(m => new MovieDto
               {
                   Id = m.Id,
                   Title = m.Title,
                   Director = m.Director.Name,
                   DirectorId = m.DirectorId,
                   Description = m.Description,
                   AvailableCount = m.AvailableCount,
                   Count = m.Count,
                   Year = m.Year,
                   MovieGenres = m.MovieGenres.Select(mg => mg.Genre.Name)
               }
               ).FirstOrDefault();

            var genres = movie.MovieGenres;

            string allgenres = null;

            foreach (var genre in genres)
            {
                allgenres += genre + ", ";
            }

            movie.GenreName = allgenres;

            if (movie == null)
                throw new EntityNotFoundException("Movie");

            return movie;
        }
    }
}
