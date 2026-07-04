
using ParkingTicketsManagment.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagment.Domain.Repositories
{
    public interface IAuthRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string Email);
    }
}
