using ParkingTicektsManagment.Domain.Repositories;
using ParkingTicketsManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public int SaveChanges() => _context.SaveChanges();

        public void Dispose() => _context.Dispose();

    }
}
