using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfUpdateDirectorCommand : EfBaseCommand, IUpdateDirectorCommand
    {
        public EfUpdateDirectorCommand(moviesContext context) : base(context)
        {
        }

        public void Execute(DirectorDto request)
        {
            var director = _context.Directors.Find(request.Id);

            if (director == null)
            {
                throw new EntityNotFoundException("Director");
            }

            if (director.Name != request.Name)
            {
                if (_context.Directors.Any(d => d.Name == request.Name))
                {
                    throw new EntityAlreadyExistsException("Director");
                }

                director.Name = request.Name;
                director.ModifiedAt = DateTime.Now;

                _context.SaveChanges();
            }
        }
    }
}
