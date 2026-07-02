using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ParkingTicektsManagment.Domain.Domains
{
    public class SubscriptionTicket
    {
        public Guid Id { get; set; }

        [Required]
        public DateTime ValidFrom { get; set; }

        [Required]
        public DateTime ValidTo { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 100000.00)]
        public decimal Price { get; set; }

        [Required]
        public Guid VehicleId { get; set; }

        [Required]
        public Guid ZoneId { get; set; }

        public Vehicle Vehicle { get; set; } = null!;
        public Zone Zone { get; set; } = null!;
    }
}
