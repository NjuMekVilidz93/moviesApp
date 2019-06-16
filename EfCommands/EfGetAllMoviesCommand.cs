using Application.Commands;
using Application.DTO;
using Application.Searches;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfGetAllMoviesCommand : EfBaseCommand, IGetAllMoviesCommand
    {
        public EfGetAllMoviesCommand(moviesContext context) : base(context)
        {
        }

        public IEnumerable<MovieDto> Execute(MovieSearch request)
        {
            var query = _context.Movies.AsQueryable();

            if (request.MovieName != null)
            {
                var keyword = request.MovieName.ToLower();
                query = query.Where(m => m.Title.ToLower().Contains(keyword));
            }
            if (request.IsAvailable)
            {
                query = query.Where(m => m.AvailableCount > 0);
            }
            if (request.MovieYear != null)
            {
                query = query.Where(m => m.Year == request.MovieYear);
            }

            return query
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
                });
        }
    }
}
