using TicketFree.Enums;

namespace TicketFree.Features.Users
{
    public class User
    {
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public ERoles UserRole { get; set; }
        public Guid UserToken { get; set; }

    }
}
