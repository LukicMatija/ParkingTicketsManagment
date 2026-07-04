using ParkingTicketsManagment.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagment.Domain.Repositories
{
    public interface ISubscriptionTicketRepository : IRepository<SubscriptionTicket>
    {
        Task<bool> IsSubsTicketValid(Guid idVehicle, Guid idZone);
    }
}
