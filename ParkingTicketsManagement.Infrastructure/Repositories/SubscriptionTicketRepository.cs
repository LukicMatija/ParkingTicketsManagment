using Microsoft.EntityFrameworkCore;
using ParkingTicketsManagement.Infrastructure.Data;
using ParkingTicketsManagment.Domain.Domains;
using ParkingTicketsManagment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.Repositories
{
    public class SubscriptionTicketRepository : Repository<SubscriptionTicket>, ISubscriptionTicketRepository
    {
        public SubscriptionTicketRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> IsSubsTicketValid(Guid idVehicle, Guid idZone)
        {
            return await DbSet.AnyAsync(sc =>
                sc.VehicleId == idVehicle &&
                sc.ZoneId == idZone &&
                sc.ValidTo > DateTime.Now);
        }
    }
}
