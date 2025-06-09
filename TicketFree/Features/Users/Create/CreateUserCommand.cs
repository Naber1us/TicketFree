using MediatR;
using TicketFree.Enums;

namespace TicketFree.Features.Users.Create
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public ERoles Role { get; set; } = ERoles.Guest;
    }

}
