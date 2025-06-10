namespace TicketFree.Features.Places
{
    public class Place
    {
        public Guid PlaceId { get; set; }
        public int PlaceCountMembers { get; set; }
        public Guid PlaceHolder { get; set; }
        public required string PlaceName { get; set; }
    }
}
