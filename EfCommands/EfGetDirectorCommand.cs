using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public class EfGetDirectorCommand : EfBaseCommand, IGetDirectorCommand
    {
        public EfGetDirectorCommand(moviesContext context) : base(context)
        {
        }

        public DirectorDto Execute(int request)
        {
            var director = _context.Directors.Find(request);

            if (director == null)
            {
                throw new EntityNotFoundException("Director");
            }
            return new DirectorDto
            {
                Id = director.Id,
                Name = director.Name
            };
        }
    }
}
