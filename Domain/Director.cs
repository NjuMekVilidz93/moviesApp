using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Director : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
