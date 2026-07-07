using ParkingTicketsManagment.Domain.Repositories;
using ParkingTicketsManagement.Infrastructure.Data;
using ParkingTicketsManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IAuthRepository? _auth;
        private IUserRepository? _users;
        private IVehicleRepository? _vehicles;
        private IZoneRepository? _zones;
        private ISubscriptionTicketRepository? _subscriptionTickets;
        private IParkingTicketRepository? _parkingTickets;
        private ILocationRepository? _locations;
        private IViolationTypeRepository? _violationTypes;
        private IPaymentRepository _payments;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IAuthRepository Auth =>
            _auth ??= new AuthRepository(_context);
        public IUserRepository Users =>
            _users ??= new UserRepository(_context);
        public IVehicleRepository Vehicles =>
            _vehicles ??= new VehicleRepository(_context);
        public IZoneRepository Zones =>
            _zones ??= new ZoneRepository(_context);
        public ISubscriptionTicketRepository SubscriptionTickets =>
            _subscriptionTickets ??= new SubscriptionTicketRepository(_context);
        public IParkingTicketRepository ParkingTickets =>
            _parkingTickets ??= new ParkingTicketRepository(_context);
        public ILocationRepository Locations =>
            _locations ??= new LocationRepository(_context);
        public IViolationTypeRepository ViolationTypes =>
            _violationTypes ??= new ViolationTypeRepository(_context);
        public IPaymentRepository Payments =>
            _payments ??= new PaymentRepository(_context);
        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();

    }
}
