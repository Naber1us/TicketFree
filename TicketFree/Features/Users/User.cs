using TicketFree.Enums;
using MediatR;

namespace TicketFree.Features.Users
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ERoles Role { get; set; }
        public Guid Token { get; set; }

    }
}
