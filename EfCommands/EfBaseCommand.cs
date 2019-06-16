using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public abstract class EfBaseCommand
    {
        protected EfBaseCommand(moviesContext context)
        {
            _context = context;
        }

        protected moviesContext _context { get; }

        
    }
}
