using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfCreateReservationCommand : EfBaseCommand, ICreateReservationCommand
    {
        public EfCreateReservationCommand(moviesContext context) : base(context)
        {
        }

        public void Execute(ReservationDto request)
        {
            if (_context.Reservations.Any(r => r.UserId == request.UserId))
            {
                throw new EntityAlreadyExistsException("Reservation");
            }

            TimeSpan addedTime = new TimeSpan(72, 0, 0);

            var reservation = new Domain.Reservation
            {
                CreatedAt = DateTime.Now,
                DateExpiry = DateTime.Now.Add(addedTime),
                UserId = request.UserId,

            };

            var movies = request.MoviesSelected;

            var allMovies = new List<string>();
            var idsForMovies = new List<int>();

            var allMoviesExists = _context.Movies.ToList();

            allMovies = movies.Split(", ").ToList();

            foreach (var m in allMovies)
            {
                foreach (var M in allMoviesExists)
                {
                    if (M.Title == m)
                    {
                        idsForMovies.Add(M.Id);
                    }
                }
            }

            var movieReservations = new List<MovieReservation>();

            foreach (int mId in idsForMovies)
            {
                var movieReservation = new Domain.MovieReservation
                {
                    ReservationId = reservation.Id,
                    MovieId = mId
                };

                movieReservations.Add(movieReservation);
            }

            reservation.MovieReservations = movieReservations;


            _context.Reservations.Add(reservation);
            _context.SaveChanges();
        }
    }
}
