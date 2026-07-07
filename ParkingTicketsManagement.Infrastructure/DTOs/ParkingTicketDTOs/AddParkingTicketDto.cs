using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.DTOs.ParkingTicketDTOs
{
    public record AddParkingTicketDto(
        string TicketNumber,
        Guid VehicleId,
        Guid LocationId,
        Guid ViolationTypeId
        );
}
