namespace TicketFree.Features.Places
{
    public class Places
    {
        public Guid PlaceId { get; set; }
        public int PlaceCountMembers { get; set; }
        public Guid PlaceHolder { get; set; }
        public required string PlaceName { get; set; }
    }
}
