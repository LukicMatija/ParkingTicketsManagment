using ParkingTicketsManagment.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagment.Domain.Repositories
{
    public interface IVehicleRepository:IRepository<Vehicle>
    {
        Task<List<Vehicle>> getAllVehiclesForUserAsync(Guid UserId);
    }
}
