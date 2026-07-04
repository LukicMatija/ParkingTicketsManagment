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

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IAuthRepository Auth =>
            _auth ??= new AuthRepository(_context);
        public IUserRepository Users =>
            _users ??= new UserRepository(_context);
        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();

    }
}
