using ParkingTicketsManagment.Infrastructure.DTOs.ZoneDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.DTOs.ZoneDTOs
{
    public record ZoneDTO(
        Guid Id,
        string Name,
        decimal PricePerHour,
        List<PolygonDto> Polygon
    );
}
