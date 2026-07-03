using Microsoft.EntityFrameworkCore;
using ParkingTicektsManagment.Domain.Domains;
using ParkingTicektsManagment.Domain.Repositories;
using ParkingTicketsManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.Repositories
{
    public class AuthRepository : Repository<User>, IAuthRepository
    {
        public AuthRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByEmailAsync(string Email)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Email == Email);
        }
    }
}
