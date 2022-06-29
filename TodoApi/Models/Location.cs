using System;
using System.Collections.Generic;

#nullable disable

namespace WeatherApi.Models
{
    public partial class Location
    {
        public Location()
        {
            Readings = new HashSet<Reading>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Reading> Readings { get; set; }
    }
}
