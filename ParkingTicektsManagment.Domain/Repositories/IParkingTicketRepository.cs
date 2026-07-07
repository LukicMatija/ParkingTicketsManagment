using ParkingTicketsManagment.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagment.Domain.Repositories
{
    public interface IParkingTicketRepository : IRepository<ParkingTicket>
    {
        Task<List<ParkingTicket>> getTicketsForVehicle(Guid VehicleId);
        Task<List<ParkingTicket>> getTicketsForUser(Guid UserId);
        Task<bool> vehicleFined(Guid VehicleId, Guid VioTypeId);

    }
}
