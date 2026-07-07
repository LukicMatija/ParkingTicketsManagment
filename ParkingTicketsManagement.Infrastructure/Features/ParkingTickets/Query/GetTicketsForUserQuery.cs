using MediatR;
using ParkingTicketsManagement.Infrastructure.DTOs.ParkingTicketDTOs;
using System;
using System.Collections.Generic;
using System.Text;


namespace ParkingTicketsManagement.Infrastructure.Features.ParkingTickets.Query
{
    public record GetTicketsForUserQuery(Guid UserId) : IRequest<List<UserTicketDetailsDto>>;

    public class GetTicketsForUserQueryHandler : IRequestHandler<GetTicketsForUserQuery, List<UserTicketDetailsDto>>
    {
        private readonly ParkingTicketsManagment.Domain.Repositories.IUnitOfWork _uow;

        public GetTicketsForUserQueryHandler(ParkingTicketsManagment.Domain.Repositories.IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<UserTicketDetailsDto>> Handle(GetTicketsForUserQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _uow.ParkingTickets.getTicketsForUser(request.UserId);
            var dtos = new List<UserTicketDetailsDto>();

            foreach (var pt in tickets)
            {
                var payments = await _uow.Payments.FindAsync(p => p.ParkingTicketId == pt.Id);
                decimal paidAmount = payments.Sum(p => p.Amount);
                decimal remainingAmount = pt.Amount - paidAmount;

                dtos.Add(new UserTicketDetailsDto(
                    pt.Id,
                    pt.TicketNumber,
                    pt.Amount,
                    remainingAmount,
                    pt.Status.ToString(),
                    pt.IssuedAt,
                    pt.ViolationType?.Name ?? string.Empty,
                    pt.ViolationType?.Description ?? string.Empty,
                    pt.Location?.StreetName ?? string.Empty,
                    pt.Location?.City ?? string.Empty,
                    pt.Worker != null ? $"{pt.Worker.FirstName} {pt.Worker.LastName}" : string.Empty,
                    pt.Vehicle?.LicensePlate ?? string.Empty,
                    pt.Vehicle?.Model ?? string.Empty 
                ));
            }

            return dtos;
        }
    }
}
