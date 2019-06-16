using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfGetOneReservationCommand : EfBaseCommand, IGetOneReservationCommand
    {
        public EfGetOneReservationCommand(moviesContext context) : base(context)
        {
        }

        public ReservationDto Execute(int request)
        {
            var reservation = _context.Reservations
                .Where(r => r.Id == request)
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

                }).FirstOrDefault();

            var movies = reservation.Movies;

            string allMovies = null;

            foreach (var movie in movies)
            {
                allMovies += movie + ", ";
            }

            reservation.MoviesSelected = allMovies;

            if (reservation == null)
            {
                throw new EntityNotFoundException("Reservation");
            }

            return reservation;
        }
    }
}
