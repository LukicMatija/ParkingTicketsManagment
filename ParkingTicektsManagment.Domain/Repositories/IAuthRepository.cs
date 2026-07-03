
using ParkingTicektsManagment.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicektsManagment.Domain.Repositories
{
    public interface IAuthRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string Email);
    }
}
