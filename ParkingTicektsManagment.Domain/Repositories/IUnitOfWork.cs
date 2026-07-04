using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagment.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthRepository Auth {  get; }
        IUserRepository Users { get; }
        Task<int> SaveChangesAsync();
    }
}
