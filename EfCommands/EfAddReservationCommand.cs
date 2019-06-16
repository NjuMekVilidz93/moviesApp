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
    public class EfAddReservationCommand : EfBaseCommand, IAddReservationCommand
    {
        public EfAddReservationCommand(moviesContext context) : base(context)
        {
        }

        public void Execute(ReservationDto request)
        {
            if (_context.Reservations.Any(r => r.UserId == request.UserId))
            {
                throw new EntityAlreadyExistsException("Reservation");
            }

            TimeSpan addedTime = new TimeSpan(72,0,0);

            var selectedMovies = request.MovieReservations;
            var reservation = new Domain.Reservation
            {
                CreatedAt = DateTime.Now,
                DateExpiry = DateTime.Now.Add(addedTime),
                UserId = request.UserId,

            };

            var movies = new List<Domain.MovieReservation>();

            foreach (var m in selectedMovies)
            {
                var movieReservation = new Domain.MovieReservation
                {
                    ReservationId = reservation.Id,
                    MovieId = m
                };
                movies.Add(movieReservation);
            }

            reservation.MovieReservations = movies;

            _context.Reservations.Add(reservation);
            _context.SaveChanges();
        }
    }
}
