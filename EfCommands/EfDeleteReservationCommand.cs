using Application.Commands;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public class EfDeleteReservationCommand : EfBaseCommand, IDeleteReservationCommand
    {
        public EfDeleteReservationCommand(moviesContext context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var reservation = _context.Reservations.Find(request);

            if (reservation == null)
            {
                throw new EntityNotFoundException("Reservation");
            }

            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
        }
    }
}
