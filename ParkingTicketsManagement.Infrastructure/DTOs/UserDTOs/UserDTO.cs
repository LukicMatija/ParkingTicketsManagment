using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.DTOs.UserDTOs
{
    public record UserDTO(
        Guid Id,
        string FirstName,
        string LastName,
        string Email
    );
}
