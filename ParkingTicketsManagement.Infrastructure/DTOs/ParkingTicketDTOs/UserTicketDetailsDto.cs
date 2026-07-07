using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.DTOs.ParkingTicketDTOs
{
    public record UserTicketDetailsDto(
        Guid TicketId,
        string TicketNumber,
        decimal TotalAmount,
        decimal RemainingAmount,
        string Status,
        DateTime IssuedAt,
        string ViolationTypeName,
        string ViolationDescription,
        string StreetName,
        string City,
        string WorkerFullName,
        string LicensePlate,
        string VehicleModel
    );
}
