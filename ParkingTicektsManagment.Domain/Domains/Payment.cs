using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ParkingTicektsManagment.Domain.Domains
{
    public class Payment
    {
        public Guid Id { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        [StringLength(100)]
        public string TransactionNumber { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        public Guid ParkingTicketId { get; set; }

        public ParkingTicket ParkingTicket { get; set; } = null!;
    }
}
