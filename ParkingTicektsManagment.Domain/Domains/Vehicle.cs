using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ParkingTicektsManagment.Domain.Domains
{
    public class Vehicle
    {
        public Guid Id { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]{2}-d{3,5}-[A-Z]{2}$", ErrorMessage = "Invalid license plate format.")]
        [StringLength(20)]
        public string LicensePlate { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Make { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Model { get; set; } = string.Empty;

        [Required]
        public Guid UserId { get; set; }

        public User User { get; set; } = null!;
        public ICollection<SubscriptionTicket> SubscriptionTickets { get; set; } = new List<SubscriptionTicket>();
        public ICollection<ParkingTicket> ParkingTickets { get; set; } = new List<ParkingTicket>();
    }
}
