using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ParkingTicketsManagment.Domain.Domains
{
    public class ParkingTicket
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(30)]
        public string TicketNumber { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.00, 50000.00)]
        public decimal Amount { get; set; }

        [Required]
        public DateTime IssuedAt { get; set; }

        [Required]
        public TicketStatus Status { get; set; }

        [Required]
        public Guid VehicleId { get; set; }

        [Required]
        public Guid LocationId { get; set; }

        [Required]
        public Guid WorkerId { get; set; }

        public Vehicle Vehicle { get; set; } = null!;
        public Location Location { get; set; } = null!;

        [ForeignKey(nameof(WorkerId))]
        public User Worker { get; set; } = null!;

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
