using MediatR;
using ParkingTicketsManagement.Infrastructure.DTOs.PaymentsDTOs;
using ParkingTicketsManagment.Domain.Domains;
using ParkingTicketsManagment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.Features.ParkingTickets.Payments.Commands
{
    public record CreatePaymentCommand(CreatePaymentDto Dto) : IRequest<Guid>;

    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Guid>
    {
        private readonly IUnitOfWork _uow;

        public CreatePaymentCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Guid> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _uow.ParkingTickets.GetByIdAsync(request.Dto.ParkingTicketId);
            if (ticket == null)
            {
                throw new Exception("The parking ticket with the provided ID does not exist.");
            }

            if (ticket.Status == TicketStatus.Paid)
            {
                throw new Exception("Cannot process payment. This parking ticket has already been fully paid.");
            }

            var existingPayments = await _uow.Payments.FindAsync(p => p.ParkingTicketId == ticket.Id);
            decimal alreadyPaid = existingPayments.Sum(p => p.Amount);
            decimal remainingAmount = ticket.Amount - alreadyPaid;

            if (request.Dto.Amount > remainingAmount)
            {
                throw new Exception($"Payment amount exceeds the remaining balance. The remaining amount to pay is {remainingAmount} RSD.");
            }

            if (request.Dto.Amount <= 0)
            {
                throw new Exception("Payment amount must be greater than zero.");
            }

            var newPayment = new Payment
            {
                Id = Guid.NewGuid(),
                ParkingTicketId = ticket.Id,
                Amount = request.Dto.Amount,
                PaymentDate = DateTime.UtcNow,
                TransactionNumber = $"TXN-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}"
            };

            await _uow.Payments.AddAsync(newPayment);

            if (request.Dto.Amount == remainingAmount)
            {
                ticket.Status = TicketStatus.Paid;
            }

            await _uow.SaveChangesAsync();

            return newPayment.Id;
        }
    }
}
