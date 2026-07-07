using ParkingTicketsManagement.Infrastructure.Data;
using ParkingTicketsManagment.Domain.Domains;
using ParkingTicketsManagment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.Repositories
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
