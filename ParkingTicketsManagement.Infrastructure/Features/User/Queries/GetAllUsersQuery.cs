using MediatR;
using ParkingTicketsManagement.Infrastructure.DTOs.UserDTOs;
using ParkingTicketsManagment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.Features.User.Queries
{
    public record GetAllUsersQuery() : IRequest<List<UserDTO>>;
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery,List<UserDTO>>
    {
        private readonly IUnitOfWork _uow;
        public GetAllUsersQueryHandler(IUnitOfWork uow) 
        { 
            _uow = uow;
        }

        public async Task<List<UserDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _uow.Users.GetAllAsync();
            return users.Select(u => new UserDTO(
                u.Id,
                u.FirstName,
                u.LastName,
                u.Email
            )).ToList();
        }
    }
}
