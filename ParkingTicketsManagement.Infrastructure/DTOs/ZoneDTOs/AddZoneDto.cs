namespace ParkingTicketsManagment.Infrastructure.DTOs.ZoneDTOs
{
    public class AddZoneDto 
    {
        public string? Name { get; set; }
        public decimal PricePerHour { get; set; }
        public List<PolygonDto>? Polygons { get; set; }
    }
}
