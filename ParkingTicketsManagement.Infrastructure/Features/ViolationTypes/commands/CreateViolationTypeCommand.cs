using MediatR;
using ParkingTicketsManagment.Domain.Domains;
using ParkingTicketsManagment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.Features.ViolationTypes.commands
{
    public record CreateViolationTypeCommand(string Name, string Description, decimal Amount) : IRequest<Guid>;
    public class CreateVilationTypeCommandHandler : IRequestHandler<CreateViolationTypeCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateVilationTypeCommandHandler(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }
        public async Task<Guid> Handle(CreateViolationTypeCommand request, CancellationToken cancellationToken)
        {
            var violationType = new ViolationType
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Amount = request.Amount
            };

            await _unitOfWork.ViolationTypes.AddAsync(violationType);
            await _unitOfWork.SaveChangesAsync();

            return violationType.Id;
        }
    }

}

