namespace TicketFree.Requests
{
    public class Places
    {
        public Guid placeId { get; set; }
        public int placeCountMembers { get; set; }
        public Guid placeHolder { get; set; }
        public string placeName { get; set; }
    }
}
