namespace TicketFree.Features.Tickets
{
    public class Ticket
    {
        public Guid TicketId { get; set; }
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
    }
}
