using MediatR;
using ParkingTicketsManagement.Infrastructure.DTOs.ViolationTypeDTOs;
using ParkingTicketsManagment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.Features.ViolationTypes.queries
{
    public record GetViolationTypeByIdQuery(Guid Id) : IRequest<ViolationTypeResponseDto?>;
    public class GetViolationTypeByIdQueryHandler : IRequestHandler<GetViolationTypeByIdQuery, ViolationTypeResponseDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetViolationTypeByIdQueryHandler(IUnitOfWork IUnitOfWork)
        {
            _unitOfWork = IUnitOfWork;
        }
        public async Task<ViolationTypeResponseDto?> Handle(GetViolationTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var type = await _unitOfWork.ViolationTypes.GetByIdAsync(request.Id);
            if (type == null)
            {
                return null;
            }
            return new ViolationTypeResponseDto
            {
                Id = type.Id,
                Name = type.Name,
                Description = type.Description,
                Amount = type.Amount
            };
        }
    }


}
