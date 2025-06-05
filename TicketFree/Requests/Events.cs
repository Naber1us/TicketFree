namespace TicketFree.Requests
{
    public class Events
    {
        public Guid eventId { get; set; }
        public Guid organizatorId { get; set; }
        public int eventCountTickets { get; set; }
        public DateTime eventStart { get; set; }
        public DateTime eventEnd { get; set; }
        public string? eventName { get; set; }
        public string? eventDescription { get; set; }
        public Guid? eventImage { get; set; }
        public Guid placeId { get; set; }
    }
}
