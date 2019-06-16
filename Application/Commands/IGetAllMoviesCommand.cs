using Application.DTO;
using Application.Interfaces;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands
{
    public interface IGetAllMoviesCommand : ICommand<MovieSearch, IEnumerable<MovieDto>>
    {
    }
}
