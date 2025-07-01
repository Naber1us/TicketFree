using MediatR;
using TicketFree.Interfaces;

namespace TicketFree.Features.Places.Create
{
    public class CreatePlaceCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<CreatePlaceCommand, Place>
    {
        public async Task<Place> Handle(CreatePlaceCommand request, CancellationToken cancellationToken)
        {
            var place = new Place
            {
                PlaceId = Guid.NewGuid(),
                PlaceName = request.PlaceName,
                PlaceCountMembers = request.PlaceCountMembers,
                PlaceHolder = request.PlaceHolder
            };

            dbContext.PlacesInfo.Add(place);
            await dbContext.SaveChangesAsync(cancellationToken);

            return place;
        }
    }
}
