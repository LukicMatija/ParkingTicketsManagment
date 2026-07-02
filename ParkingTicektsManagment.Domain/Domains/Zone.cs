using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using NetTopologySuite.Geometries;

namespace ParkingTicektsManagment.Domain.Domains
{
    public class Zone
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 500.00)]
        public decimal PricePerHour { get; set; }

        [Required]
        public Polygon ZoneBoundaries { get; set; } = null!;

        public ICollection<Location> Locations { get; set; } = new List<Location>();
        public ICollection<SubscriptionTicket> SubscriptionTickets { get; set; } = new List<SubscriptionTicket>();
    }
}
