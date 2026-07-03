using ParkingTicektsManagment.Domain.Repositories;
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

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IAuthRepository Auth =>
            _auth ??= new AuthRepository(_context);
        public int SaveChanges() => _context.SaveChanges();

        public void Dispose() => _context.Dispose();

    }
}
