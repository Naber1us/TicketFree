namespace TicketFree.Features.Events
{
    public class Event
    {
        public Guid EventId { get; set; }
        public Guid OrganizatorId { get; set; }
        public int EventCountTickets { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }
        public required string EventName { get; set; }
        public string EventDescription { get; set; } = string.Empty;
        public Guid EventImage { get; set; }
        public string EventStatus { get; set; } = string.Empty;
        public Guid PlaceId { get; set; }
    }
}
