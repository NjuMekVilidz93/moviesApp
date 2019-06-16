using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public class EfGetUserCommand : EfBaseCommand, IGetUserCommand
    {
        public EfGetUserCommand(moviesContext context) : base(context)
        {
        }

        public UserDto Execute(int request)
        {
            var user = _context.Users.Find(request);

            if (user == null)
            {
                throw new EntityNotFoundException("User");
            }

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                Role = user.Role.Name,
                RoleId = user.RoleId,
                Username = user.Username
            };
        }
    }
}
