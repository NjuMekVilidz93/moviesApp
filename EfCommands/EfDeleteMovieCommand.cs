using Application.Commands;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public class EfDeleteMovieCommand : EfBaseCommand, IDeleteMovieCommand
    {
        public EfDeleteMovieCommand(moviesContext context) : base(context)
        { }
        public void Execute(int request)
        {
            var movie = _context.Movies.Find(request);

            if (movie == null)
                throw new EntityNotFoundException("Movie");
           

            _context.Movies.Remove(movie);

            _context.SaveChanges();
        }
    }
}
