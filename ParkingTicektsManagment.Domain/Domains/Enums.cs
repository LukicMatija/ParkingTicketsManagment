using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicektsManagment.Domain.Domains
{
    public enum Role
    {
        Admin = 1,
        Worker = 2,
        User = 3
    }

    public enum TicketStatus
    {
        Unpaid = 1,
        Paid = 2
    }
}
