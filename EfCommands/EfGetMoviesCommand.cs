using Application.Commands;
using Application.DTO;
using Application.Searches;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Application.Responses;

namespace EfCommands
{
    public class EfGetMoviesCommand : EfBaseCommand, IGetMoviesCommand
    {
        public EfGetMoviesCommand(moviesContext context) : base(context)
        {

        }

        public PagedResponse<MovieDto> Execute(MovieSearch request)
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
            if (request.GenreId != null)
            {
                query = query.Where(m => m.MovieGenres.Any(mg => mg.GenreId == request.GenreId));
            }

            var totalCount = query.Count();

            query =  query
                .Include(m => m.Director)
                .ThenInclude(d => d.Name)
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre).Skip((request.PageNumber - 1)* request.PerPage).Take(request.PerPage);

            var pagesCount = (int)Math.Ceiling((double)totalCount / request.PerPage);

            var response = new PagedResponse<MovieDto>
            {
                CurrentPage = request.PageNumber,
                TotalCount = query.Count(),
                PagesCount = pagesCount,
                Data = query.Select(m => new MovieDto
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
                )
        };


            return response;
            
        }

        public object Execute(int id)
        {
            throw new NotImplementedException();
        }
    }
}
