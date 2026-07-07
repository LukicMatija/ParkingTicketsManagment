using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.DTOs.LocationDTOs
{
    public class LocationResponseDto
    {
        public Guid Id { get; set; }
        public string StreetName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public Guid ZoneId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
