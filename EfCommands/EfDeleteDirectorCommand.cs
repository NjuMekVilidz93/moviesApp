using Application.Commands;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public class EfDeleteDirectorCommand : EfBaseCommand, IDeleteDirectorCommand
    {
        public EfDeleteDirectorCommand(moviesContext context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var director = _context.Directors.Find(request);

            if (director == null)
                throw new EntityNotFoundException("Director");

            _context.Directors.Remove(director);

            _context.SaveChanges();
        }
    }
}
