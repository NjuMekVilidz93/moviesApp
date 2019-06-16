using Application.Commands;
using Application.DTO;
using Application.Searches;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfGetDirectorsCommand : EfBaseCommand, IGetDirectorsCommand
    {
        public EfGetDirectorsCommand(moviesContext context) : base(context)
        { }

        public IEnumerable<DirectorDto> Execute(DirectorSearch request)
        {
            var query = _context.Directors.AsQueryable();
           
            if (request.Name != null)
            {
                var keyword = request.Name.ToLower();
                query = query.Where(d => d.Name.ToLower().Contains(keyword));
            }

            return query.Select(d => new DirectorDto
            {
                Id = d.Id,
                Name = d.Name
            });
        }
    }
}
