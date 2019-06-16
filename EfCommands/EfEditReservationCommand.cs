using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using Domain;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfEditReservationCommand : EfBaseCommand, IEditReservationCommand
    {
        public EfEditReservationCommand(moviesContext context) : base(context)
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


            var movieSelected = reservation.MovieReservations;

            foreach (Domain.MovieReservation mr in movieSelected)
            {
                _context.Entry(mr).State = EntityState.Deleted;
            }

            _context.SaveChanges();

            var movieReservations = new List<MovieReservation>();

            var movie = request.MoviesSelected;

            var allMovies = new List<string>();

            var idsForMovies = new List<int>();

            var allMoviesExists = _context.Movies.ToList();

            allMovies = movie.Split(", ").ToList();

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

            foreach (int mId in idsForMovies)
            {
                var movieR = new Domain.MovieReservation
                {
                    ReservationId = reservation.Id,
                    MovieId = mId
                };

                movieReservations.Add(movieR);
            }

            reservation.MovieReservations = movieReservations;
            reservation.UserId = request.UserId;
            reservation.ModifiedAt = DateTime.Now;
            _context.SaveChanges();
        }
    }
}
