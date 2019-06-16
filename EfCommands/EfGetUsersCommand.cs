using Application.Commands;
using Application.DTO;
using Application.Searches;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfGetUsersCommand : EfBaseCommand, IGetUsersCommand
    {
        public EfGetUsersCommand(moviesContext context) : base(context)
        {
        }

        public IEnumerable<UserDto> Execute(UserSearch request)
        {
            var query = _context.Users.AsQueryable();

            if (request.FirstName != null)
            {
                var keyword = request.FirstName.ToLower();
                query = query.Where(u => u.FirstName.ToLower().Contains(keyword));
            }

            if (request.LastName != null)
            {
                var keyword = request.LastName.ToLower();
                query = query.Where(u => u.LastName.ToLower().Contains(keyword));
            }

            if (request.Username != null)
            {
                var keyword = request.Username.ToLower();
                query = query.Where(u => u.Username.ToLower().Contains(keyword));
            }

            return query
                .Include(u => u.Role)
                .ThenInclude(r => r.Name)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Username = u.Username,
                    Role = u.Role.Name
                });


        }
    }
}
