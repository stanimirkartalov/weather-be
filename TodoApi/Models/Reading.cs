using System;
using System.Collections.Generic;

#nullable disable

namespace WeatherApi.Models
{
    public partial class Reading
    {
        public uint Id { get; set; }
        public double? Temperature { get; set; }
        public double? Humidity { get; set; }
        public double? AthmosphericPreasure { get; set; }
        public double? Lightness { get; set; }
        public int? Timestamp { get; set; }
        public int LocationId { get; set; }

        public virtual Location Location { get; set; }
    }
}
