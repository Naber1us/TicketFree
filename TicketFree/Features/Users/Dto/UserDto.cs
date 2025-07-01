using MediatR;
using TicketFree.Enums;

namespace TicketFree.Features.Users.Dto
{
    public record UserDto
    {
        public string? UserName { get; set; } = string.Empty;
        public ERoles UserRole { get; set; } = ERoles.Guest;
        public Guid UserId { get; set; } = Guid.NewGuid();
    }

    public record GetUserByIdQuery(Guid Id) : IRequest<UserDto?>;
    public record GetAllUsersQuery : IRequest<List<UserDto>>;
}
