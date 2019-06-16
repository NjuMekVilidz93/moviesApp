using Application.Commands;
using Application.DTO;
using Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfCreateMovieCommand : EfBaseCommand, ICreateMovieCommand
    {
        public EfCreateMovieCommand(moviesContext context) : base(context)
        {
        }

        public void Execute(MovieDto request)
        {
            if (!(_context.Directors.Any(d => d.Id == request.DirectorId)))
            {
                throw new Exception();
            }

            if (_context.Movies.Any(m => m.Title == request.Title))
            {
                throw new Exception();
            }



            var movie = new Domain.Movie
            {
                Title = request.Title,
                Description = request.Description,
                AvailableCount = request.AvailableCount,
                Count = request.Count,
                Year = request.Year,
                DirectorId = request.DirectorId,
                CreatedAt = DateTime.Now
            };

            var genre = request.GenreName;

            var allGenres = new List<string>();

            var idsForGenres = new List<int>();

            var allGenresExists = _context.Genres.ToList();

            allGenres = genre.Split(", ").ToList();

            foreach (var g in allGenres)
            {
                foreach (var G in allGenresExists)
                {
                    if (G.Name == g)
                    {
                        idsForGenres.Add(G.Id);
                    }
                }
            }

            var movieGenres = new List<MovieGenre>();

            foreach (int gId in idsForGenres)
            {
                var movieGenre = new Domain.MovieGenre
                {
                    MovieId = movie.Id,
                    GenreId = gId
                };

                movieGenres.Add(movieGenre);
            }




            movie.MovieGenres = movieGenres;

            _context.Movies.Add(movie);

            _context.SaveChanges();



        }


    }
}
