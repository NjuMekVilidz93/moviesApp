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
    public class EfGetReservationsCommand : EfBaseCommand, IGetReservationsCommand
    {
        public EfGetReservationsCommand(moviesContext context) : base(context)
        {
        }

        public IEnumerable<ReservationDto> Execute(ReservationSearch request)
        {
            var query = _context.Reservations.AsQueryable();

            //var user = _context.Users.Where(u => u.Username == request.Username).ToString();

            if (request.Username != null)
            {
                var keyword = request.Username.ToLower();
                query = query.Where(r => r.User.Username.ToLower().Contains(keyword));
            }

            return query
                .Include(r => r.MovieReservations)
                .ThenInclude(mr => mr.Movie)
                .Include(r => r.User)
                .ThenInclude(u => u.Username)
                .Select(r => new ReservationDto
                {
                    Id = r.Id,
                    DateCreated = r.CreatedAt,
                    DateExpiry = r.DateExpiry,
                    DateTaken = r.DateTaken,
                    DateToReturn = r.DateToReturn,
                    Username = r.User.Username,
                    UserId = r.User.Id,
                    Movies = r.MovieReservations.Select(mr => mr.Movie.Title)

                });
        }
    }
}
