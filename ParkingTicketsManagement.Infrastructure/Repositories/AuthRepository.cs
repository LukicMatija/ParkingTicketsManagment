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

        public User? GetByEmail(string Email)
        {
            return DbSet.FirstOrDefault(x => x.Email == Email);
        }
    }
}
