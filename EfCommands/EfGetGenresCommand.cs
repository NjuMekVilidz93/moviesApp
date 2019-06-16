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
    public class EfGetGenresCommand : EfBaseCommand, IGetGenresCommand
    {
        public EfGetGenresCommand(moviesContext context) : base(context)
        {
        }

        public IEnumerable<GenreDto> Execute(GenreSearch request)
        {
            var query = _context.Genres.AsQueryable();

            if (request.Name != null)
            {
                var keyword = request.Name.ToLower();
                query = query.Where(d => d.Name.ToLower().Contains(keyword));
            }
            if (request.OnlyActive.HasValue)
            {
                query = query.Where(g => g.IsDeleted != request.OnlyActive);
            }

            return query.Select(g => new GenreDto
            {

                Id = g.Id,
                Name = g.Name
            });

        }
    }
}
