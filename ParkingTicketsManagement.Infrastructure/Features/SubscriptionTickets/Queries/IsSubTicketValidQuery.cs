using MediatR;
using ParkingTicketsManagment.Domain.Domains;
using ParkingTicketsManagment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.Features.SubscriptionTickets.Queries
{
    public record IsSubsTicketValidQuery(Guid VehicleId, Guid ZoneId) : IRequest<bool>;
    public class IsSubsTicketValidQueryHandler : IRequestHandler<IsSubsTicketValidQuery, bool>
    {
        private readonly IUnitOfWork _uow;

        public IsSubsTicketValidQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<bool> Handle(IsSubsTicketValidQuery request, CancellationToken cancellationToken)
        {
            Zone? z = await _uow.Zones.GetByIdAsync(request.ZoneId);
            if (z == null)
            {
                throw new Exception("Zone not found");
            }

            return await _uow.SubscriptionTickets.IsSubsTicketValid(request.VehicleId, request.ZoneId);
        }
    }
}
