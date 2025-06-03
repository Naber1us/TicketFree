using TicketFree.Enums;

namespace TicketFree.Requests
{
    public class AddUser
    {
        public Guid UserId { get; set; }
        public string? Name { get; set; }

        public ERoles Role { get; set; }

        public Guid Token { get; set; }


    }
}
