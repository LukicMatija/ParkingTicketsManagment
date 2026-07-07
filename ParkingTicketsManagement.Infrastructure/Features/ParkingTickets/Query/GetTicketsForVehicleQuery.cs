using MediatR;
using ParkingTicketsManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.Features.ParkingTickets.Query
{
    public record VehicleTicketDetailsDto(
        Guid TicketId,
        string TicketNumber,
        decimal TotalAmount,
        decimal RemainingAmount,
        string Status,
        DateTime IssuedAt,
        string ViolationTypeName,
        string ViolationDescription,
        string StreetName,
        string City,
        string WorkerFullName
    );

    public record GetTicketsForVehicleQuery(Guid VehicleId) : IRequest<List<VehicleTicketDetailsDto>>;

    public class GetTicketsForVehicleQueryHandler : IRequestHandler<GetTicketsForVehicleQuery, List<VehicleTicketDetailsDto>>
    {
        private readonly ParkingTicketsManagment.Domain.Repositories.IUnitOfWork _uow;

        public GetTicketsForVehicleQueryHandler(ParkingTicketsManagment.Domain.Repositories.IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<VehicleTicketDetailsDto>> Handle(GetTicketsForVehicleQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _uow.ParkingTickets.getTicketsForVehicle(request.VehicleId);
            var dtos = new List<VehicleTicketDetailsDto>();

            foreach (var pt in tickets)
            {
                var payments = await _uow.Payments.FindAsync(p => p.ParkingTicketId == pt.Id);
                decimal paidAmount = payments.Sum(p => p.Amount);
                decimal remainingAmount = pt.Amount - paidAmount;

                dtos.Add(new VehicleTicketDetailsDto(
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
                    pt.Worker != null ? $"{pt.Worker.FirstName} {pt.Worker.LastName}" : string.Empty
                ));
            }

            return dtos;
        }
    }
}
