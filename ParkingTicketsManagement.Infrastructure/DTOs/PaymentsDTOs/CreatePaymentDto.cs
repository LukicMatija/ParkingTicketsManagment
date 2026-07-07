using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.DTOs.PaymentsDTOs
{
    public record CreatePaymentDto(
        Guid ParkingTicketId,
        decimal Amount
    );
}
