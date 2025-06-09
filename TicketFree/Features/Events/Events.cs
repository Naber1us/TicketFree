namespace TicketFree.Features.Events
{
    public class Events
    {
        public Guid EventId { get; set; }
        public Guid OrganizatorId { get; set; }
        public int EventCountTickets { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }
        public string? EventName { get; set; }
        public string? EventDescription { get; set; }
        public Guid? EventImage { get; set; }
        public Guid PlaceId { get; set; }
    }
}
