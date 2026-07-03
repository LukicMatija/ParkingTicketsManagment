using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicektsManagment.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthRepository Auth {  get; }
        Task<int> SaveChangesAsync();
    }
}
