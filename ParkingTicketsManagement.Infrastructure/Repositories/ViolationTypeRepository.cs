using ParkingTicketsManagement.Infrastructure.Data;
using ParkingTicketsManagment.Domain.Domains;
using ParkingTicketsManagment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.Repositories
{
    public class ViolationTypeRepository : Repository<ViolationType>, IViolationTypeRepository
    {
        public ViolationTypeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
