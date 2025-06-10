using MediatR;
using TicketFree.Enums;

namespace TicketFree.Features.Users.Get
{
    public class GetUserCommand : IRequest<User>
    {
        public string UserName { get; } = string.Empty;
        public ERoles UserRole { get; } = ERoles.Guest;
        public Guid UserId { get; } = Guid.Empty;
    }

}
