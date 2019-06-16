using Application.Commands;
using Application.Interfaces;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfMovieUserCommand : EfBaseCommand, IMovieUserCommand
    {
        private readonly IEmailSender _emailSender;

        public EfMovieUserCommand(moviesContext context, IEmailSender emailSender) : base(context)
        {
            _emailSender = emailSender;
        }

       

        public void Execute(int request)
        {
            var reservation = _context.Reservations.Find(request);

            reservation.DateTaken = DateTime.Now;
            TimeSpan addedTime = new TimeSpan(404,0,0);

            reservation.DateToReturn = DateTime.Now.Add(addedTime);

            var reserved = _context.Reservations.Include(r => r.MovieReservations).ThenInclude(br => br.Movie)
                .Where(m => m.Id == request).FirstOrDefault();

            var re = reserved.MovieReservations;

            var movie = new Domain.Movie();

            foreach (var r in re)
            {
                movie = _context.Movies.Find(r.MovieId);
                movie.AvailableCount = movie.AvailableCount - 1;
            }


            _context.SaveChanges();

            _emailSender.Body = "Succesfully,you must return movie until" + reservation.DateToReturn;
            _emailSender.Subject = "Success";
            _emailSender.ToEmail = "netcoreict@gmail.com";
            _emailSender.Send();
        }
    }
}
