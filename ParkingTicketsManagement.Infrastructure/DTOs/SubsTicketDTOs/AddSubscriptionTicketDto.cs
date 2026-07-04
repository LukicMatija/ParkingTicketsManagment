using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.DTOs.SubsTicketDTOs
{
    public record AddSubscriptionTicketDto(
        Guid VehicleId,
        double Latitude,
        double Longitude);
}
