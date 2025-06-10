using MediatR;

namespace TicketFree.Features.Places.Dto
{
    public record PlaceDto
    {
        public Guid PlaceId { get; set; } = Guid.Empty;
        public int PlaceCountMembers { get; set; }
        public Guid PlaceHolder { get; set; } = Guid.Empty;
        public string PlaceName { get; set; } = string.Empty;
    }

    public record GetPlaceByIdQuery(Guid Id) : IRequest<PlaceDto?>;
    public record GetAllPlacesQuery : IRequest<List<PlaceDto>>;
}
