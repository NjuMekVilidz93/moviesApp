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
    public class EfUpdateUserCommand : EfBaseCommand, IUpdateUserCommand
    {
        public EfUpdateUserCommand(moviesContext context) : base(context)
        {
        }

        public void Execute(UserDto request)
        {
            var user = _context.Users.Find(request.Id);

            if (user == null)
            {
                throw new EntityNotFoundException("User");
            }
            bool isChanged = false;

            if (user.FirstName != request.FirstName)
            {
                isChanged = true;
            }
            if (user.LastName != request.LastName)
            {
                isChanged = true;
            }
            if (user.Username != request.Username)
            {
                isChanged = true;
            }
            if (user.RoleId != request.RoleId)
            {
                isChanged = true;
            }
            if (isChanged)
            {


                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Username = request.Username;
                user.RoleId = request.RoleId;
                user.ModifiedAt = DateTime.Now;

                _context.SaveChanges();
            }


        }
    }
}
