using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using Domain;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfEditMovieCommand : EfBaseCommand, IEditMovieCommand
    {
        public EfEditMovieCommand(moviesContext context) : base(context)
        {
        }

        public void Execute(MovieDto request)
        {
            var movie = _context.Movies.Where(m => m.Id == request.Id)
                .Include(mg => mg.MovieGenres)
                .ThenInclude(m => m.Genre)
                .FirstOrDefault();

            bool IsChanged = false;

            if (movie == null)
            {
                throw new EntityNotFoundException("Movie");
            }

            if (movie.Title != request.Title)
                IsChanged = true;
            if (movie.Year != request.Year)
                IsChanged = true;
            if (movie.DirectorId != request.DirectorId)
              IsChanged = true;

            if (IsChanged)
            {
                movie.Title = request.Title;
                movie.Description = request.Description;
                movie.Year = request.Year;
                movie.DirectorId = request.DirectorId;
                movie.AvailableCount = request.AvailableCount;
                movie.Count = request.Count;
                movie.ModifiedAt = DateTime.Now;

                var movieGenreSelected = movie.MovieGenres;


                foreach (Domain.MovieGenre mg in movieGenreSelected)
                {
                    _context.Entry(mg).State = EntityState.Deleted;
                }

                _context.SaveChanges();

                var movieGenres = new List<MovieGenre>();

                var genre = request.GenreName;

                var allGenres = new List<string>();

                var idsForGenres = new List<int>();

                var allGenresThatExists = _context.Genres.ToList();

                allGenres = genre.Split(", ").ToList();

                foreach (var g in allGenres)
                {
                    foreach (var G in allGenresThatExists)
                    {
                        if (G.Name == g)
                        {
                            idsForGenres.Add(G.Id);
                        }
                    }
                }

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

                _context.SaveChanges();




            }

        }
    }
}
