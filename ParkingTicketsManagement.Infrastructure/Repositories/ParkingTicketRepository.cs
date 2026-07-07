using Microsoft.EntityFrameworkCore;
using ParkingTicketsManagement.Infrastructure.Data;
using ParkingTicketsManagment.Domain.Domains;
using ParkingTicketsManagment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.Repositories
{
    public class ParkingTicketRepository : Repository<ParkingTicket>, IParkingTicketRepository
    {
        public ParkingTicketRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<ParkingTicket>> getTicketsForUser(Guid UserId)
        {
            return await DbSet.Include(pt => pt.Vehicle).Where(pt => pt.Vehicle.UserId == UserId).ToListAsync();
        }

        public async Task<List<ParkingTicket>> getTicketsForVehicle(Guid VehicleId)
        {
            return await DbSet.Where(pt => pt.VehicleId == VehicleId).ToListAsync();
        }

        //Ako vrati false moze da se izda karta
        public async Task<bool> vehicleFined(Guid VehicleId, Guid VioTypeId)
        {
            var oneHourAgo = DateTime.Now.AddHours(-1);
            return await DbSet.AnyAsync(pt =>
                pt.VehicleId == VehicleId &&
                pt.ViolationTypeId == VioTypeId &&
                pt.IssuedAt > oneHourAgo);
        }
    }
}
