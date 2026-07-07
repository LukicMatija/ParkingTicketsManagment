using MediatR;
using ParkingTicketsManagement.Infrastructure.DTOs.ViolationTypeDTOs;
using ParkingTicketsManagment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.Features.ViolationTypes.queries
{
    public record GetAllViolationTypesQuery() : IRequest<IEnumerable<ViolationTypeResponseDto>>;
    public class GetAllViolationTypesQueryHandler : IRequestHandler<GetAllViolationTypesQuery, IEnumerable<ViolationTypeResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllViolationTypesQueryHandler(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }
        public async Task<IEnumerable<ViolationTypeResponseDto>> Handle(GetAllViolationTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await _unitOfWork.ViolationTypes.GetAllAsync();
            return types.Select(t => new ViolationTypeResponseDto
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                Amount = t.Amount
            });
        }
    }


}
