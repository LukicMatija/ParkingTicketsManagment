using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using NetTopologySuite.Geometries;


namespace ParkingTicektsManagment.Domain.Domains
{
    public class Location
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string StreetName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string City { get; set; } = string.Empty;

        [Required]
        public Guid ZoneId { get; set; }

        [Required]
        public Point Coordinates { get; set; } = null!;

        public Zone Zone { get; set; } = null!;
        public ICollection<ParkingTicket> ParkingTickets { get; set; } = new List<ParkingTicket>();
    }
}
}
