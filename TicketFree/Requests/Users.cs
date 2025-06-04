using TicketFree.Enums;

namespace TicketFree.Requests
{
    public class Users
    {
        public Guid userId { get; set; }
        public string? userName { get; set; }
        public ERoles userRole { get; set; }
        public Guid userToken { get; set; }

    }
}
