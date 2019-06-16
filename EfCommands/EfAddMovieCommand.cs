using Application.Commands;
using Application.DTO;
using Application.Interfaces;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfAddMovieCommand : EfBaseCommand, IAddMovieCommand
    {
        public EfAddMovieCommand(moviesContext context, IEmailSender emailSender) : base(context)
        {
            _emailSender = emailSender;
        }
        private readonly IEmailSender _emailSender;
        public void Execute(MovieDto request)
        {
            if (!(_context.Directors.Any(d => d.Id == request.DirectorId)))
            {
                throw new Exception();
            }

            //if (!(_context.Genres.Any(g => g.Id == request.GenreId)))
            //{
            //    throw new Exception();
            //}

            if (_context.Movies.Any(m => m.Title == request.Title))
            {
                throw new Exception();
            }

            var genres = request.SelectedGenres;

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

            var movieGenres = new List<Domain.MovieGenre>();

            foreach (int g in genres)
            {
                var movieGenre = new Domain.MovieGenre
                {
                    MovieId = movie.Id,
                    GenreId = g
                };

                movieGenres.Add(movieGenre);

            }


            movie.MovieGenres = movieGenres;

            _context.Movies.Add(movie);
            _context.SaveChanges();

            _emailSender.Body = "Successfuly added movie";
            _emailSender.Subject = "Success";
            _emailSender.ToEmail = "netcoreict@gmail.com";
            _emailSender.Send();

        }

    }
}
