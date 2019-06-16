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
    public class EfAddDirectorCommand : EfBaseCommand, IAddDirectorCommand
    {
        public EfAddDirectorCommand(moviesContext context) : base(context)
        { }

        public void Execute(DirectorDto request)
        {
            if (_context.Directors.Any(d => d.Name == request.Name))
            {
                throw new EntityAlreadyExistsException("Director");
            }

            _context.Directors.Add(new Domain.Director
            {
                Name = request.Name,
                CreatedAt = DateTime.Now
            });

            _context.SaveChanges();
        }
    }
}
