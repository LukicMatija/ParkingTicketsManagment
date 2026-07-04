using MediatR;
using ParkingTicketsManagment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using ParkingTicketsManagment.Domain.Domains;

namespace ParkingTicketsManagement.Infrastructure.Features.SubscriptionTickets.Commands
{

    public record AddSubscriptionTicketCommand(Guid VehicleId, double Longitude, double Latitude) : IRequest<Guid>;
    public class AddSubscriptionTicketCommandHandler : IRequestHandler<AddSubscriptionTicketCommand, Guid>
    {
        private readonly IUnitOfWork _uow;
        public AddSubscriptionTicketCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<Guid> Handle(AddSubscriptionTicketCommand request, CancellationToken cancellationToken)
        {
            Zone? z = await _uow.Zones.FindByPoint(request.Latitude, request.Longitude);
            if (z == null)
            {
                throw new Exception("Zone not found");
            }
            if(await _uow.SubscriptionTickets.IsSubsTicketValid(request.VehicleId, z.Id))
            {
                throw new Exception("Vehicle already has subs ticket for this zone");
            }
            SubscriptionTicket sc = new SubscriptionTicket { 
                Id = Guid.NewGuid(),
                VehicleId = request.VehicleId,
                ZoneId = z.Id,
                Price = z.PricePerHour*2,
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now.AddHours(2)
            };
            await _uow.SubscriptionTickets.AddAsync(sc);
            await _uow.SaveChangesAsync();
            return sc.Id;
        }
    }
}
