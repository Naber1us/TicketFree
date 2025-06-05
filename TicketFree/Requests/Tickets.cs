namespace TicketFree.Requests
{
    public class Tickets
    {
        public Guid ticketId { get; set; }
        public Guid userId { get; set; }
        public Guid eventId { get; set; }
    }
}
