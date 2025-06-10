using MediatR;


namespace TicketFree.Features.Places.Get
{
    public class GetPlaceCommand : IRequest<Place>
    {
        public Guid PlaceId { get; } = Guid.Empty;
        public Guid PlaceHolder { get; } = Guid.Empty;
        public string PlaceName { get; } = string.Empty;
        public int PlaceCountMembers { get; } = 0;
        
    }


}
