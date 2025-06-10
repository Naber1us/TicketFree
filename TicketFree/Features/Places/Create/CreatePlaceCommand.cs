using MediatR;

namespace TicketFree.Features.Places.Create
{
    public class CreatePlaceCommand : IRequest<Place>
    {
        public Guid PlaceHolder { get; set; } = Guid.Empty;
        public int PlaceCountMembers { get; set; } = 0;
        public string PlaceName { get; set; } = string.Empty;
    }
}
