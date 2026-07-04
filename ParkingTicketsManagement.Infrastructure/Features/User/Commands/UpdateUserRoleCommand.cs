using MediatR;
using ParkingTicketsManagment.Domain.Domains;
using ParkingTicketsManagment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagment.Infrastructure.Features.User.Commands
{

    public record UpdateUserRoleCommand(Guid UserId, Role NewRole) : IRequest<Guid>;
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserRoleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {

            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new Exception($"Korisnik sa ID-jem {request.UserId} ne postoji u sistemu.");
            }

            user.Role = request.NewRole;

            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return user.Id;
        }
    }
}
