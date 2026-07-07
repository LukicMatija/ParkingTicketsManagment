using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagment.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthRepository Auth {  get; }
        IUserRepository Users { get; }
        IZoneRepository Zones { get; }
        ISubscriptionTicketRepository SubscriptionTickets{ get; }
        IVehicleRepository Vehicles{ get; }
        IParkingTicketRepository ParkingTickets { get; }
        ILocationRepository Locations { get; }
        IViolationTypeRepository ViolationTypes { get; }
        IPaymentRepository Payments { get; }
        Task<int> SaveChangesAsync();
        
    }
}
