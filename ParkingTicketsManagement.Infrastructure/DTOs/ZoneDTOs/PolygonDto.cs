using System.ComponentModel.DataAnnotations;

namespace ParkingTicketsManagment.Infrastructure.DTOs.ZoneDTOs
{
    public class PolygonDto
    {
        [Range(-90.0, 90.0, ErrorMessage = "Latitude must be between -90 and 90")]
        public double Latitude { get; set; }
        [Range(-90.0, 90.0, ErrorMessage = "Longitude must be between -90 and 90")]

        public double Longitude { get; set; }
    }
}
