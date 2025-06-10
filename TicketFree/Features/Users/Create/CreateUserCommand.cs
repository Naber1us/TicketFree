using MediatR;
using TicketFree.Enums;

namespace TicketFree.Features.Users.Create
{
    public class CreateUserCommand : IRequest<User>
    {
        public string UserName { get; set; } = string.Empty;
        public ERoles UserRole { get; set; } = ERoles.Guest;
    }

}
