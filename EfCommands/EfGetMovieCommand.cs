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
    public class EfGetMovieCommand : EfBaseCommand, IGetMovieCommand
    {
        public EfGetMovieCommand(moviesContext context) : base(context)
        { }
        

        public MovieDto Execute(int request)
        {
            
            var  movie = _context.Movies
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
                    Description = m.Description,
                    AvailableCount = m.AvailableCount,
                    Count = m.Count,
                    Year = m.Year,
                    MovieGenres = m.MovieGenres.Select(mg => mg.Genre.Name)
                }
                ).FirstOrDefault();
            if (movie == null)
                throw new EntityNotFoundException("Movie");

            return movie;
        }
    }
}
