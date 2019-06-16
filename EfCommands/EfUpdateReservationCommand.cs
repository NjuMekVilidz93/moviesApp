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
    public class EfUpdateReservationCommand : EfBaseCommand, IUpdateReservationCommand
    {
        public EfUpdateReservationCommand(moviesContext context) : base(context)
        {
        }

        public void Execute(ReservationDto request)
        {
            var reservation = _context.Reservations
                .Where(r => r.Id == request.Id)
                .Include(r => r.MovieReservations)
                .ThenInclude(mr => mr.Movie)
                .FirstOrDefault();

            

            if (reservation == null)
            {
                throw new EntityNotFoundException("Reservation");
            }

            var movieReservations = new List<Domain.MovieReservation>();

            var movies = request.MovieReservations;

            var selectedMovies = reservation.MovieReservations;

            foreach(Domain.MovieReservation mr in selectedMovies)
            {
                _context.Entry(mr).State = EntityState.Deleted;
            }

            _context.SaveChanges();

            foreach (int m in movies)
            {
                var movieReservation = new Domain.MovieReservation
                {
                    MovieId = m,
                    ReservationId = reservation.Id

                };
                movieReservations.Add(movieReservation);
            }

            reservation.MovieReservations = movieReservations;
            reservation.ModifiedAt = DateTime.Now;
            _context.SaveChanges();

        }
    }
}
