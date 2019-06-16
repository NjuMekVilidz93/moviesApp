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
    public class EfAddUserCommand : EfBaseCommand, IAddUserCommand
    {
        public EfAddUserCommand(moviesContext context) : base(context)
        {
        }

        public void Execute(UserDto request)
        {
            if (_context.Users.Any(u => u.Username == request.Username))
            {
                throw new EntityAlreadyExistsException("User with same username");
            }

            _context.Users.Add(new Domain.User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Password = request.Password,
                RoleId = request.RoleId,
                CreatedAt = DateTime.Now
            });

            _context.SaveChanges();
        }
    }
}
