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
    public class EfUpdateMovieCommand : EfBaseCommand, IUpdateMovieCommand
    {
        public EfUpdateMovieCommand(moviesContext context) : base(context)
        {
        }

        public void Execute(MovieDto request)
        {
            var movie = _context.Movies.Where(m => m.Id == request.Id)
                .Include(mg => mg.MovieGenres)
                .ThenInclude(m => m.Genre)
                .FirstOrDefault();

            //var movie = _context.Movies.Find(request.Id);

            bool IsChanged = false;

            if(movie == null)
            {
                throw new EntityNotFoundException("Movie");
            }

            if (movie.Title != request.Title)
                IsChanged = true;
            if (movie.Year != request.Year)
                IsChanged = true;
            if (movie.DirectorId != request.DirectorId)
                IsChanged = true;

            if(IsChanged)
            {
                movie.Title = request.Title;
                movie.Description = request.Description;
                movie.Year = request.Year;
                movie.DirectorId = request.DirectorId;
                movie.AvailableCount = request.AvailableCount;
                movie.Count = request.Count;
                movie.ModifiedAt = DateTime.Now;

                var movieGenres = new List<Domain.MovieGenre>();
                var genres = request.SelectedGenres;

                var selectedGenres = movie.MovieGenres;

                foreach (Domain.MovieGenre mg in selectedGenres)
                {
                    _context.Entry(mg).State = EntityState.Deleted;
                }
                _context.SaveChanges();
   
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
                _context.SaveChanges();

            }







        }
    }
}
