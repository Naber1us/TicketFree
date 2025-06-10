using MediatR;
using TicketFree.Features.Places.Dto;
using Microsoft.EntityFrameworkCore;
using TicketFree.Interfaces;

namespace TicketFree.Features.Places.Get
{
    public class GetPlaceIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetPlaceByIdQuery, PlaceDto?>
    {
        private readonly IApplicationDbContext _context = context;

        public async Task<PlaceDto?> Handle(GetPlaceByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.PlacesInfo
                .Where(p => p.PlaceId == request.Id)
                .Select(p => new PlaceDto
                {
                    PlaceId = p.PlaceId,
                    PlaceCountMembers = p.PlaceCountMembers,
                    PlaceName = p.PlaceName,
                    PlaceHolder = p.PlaceHolder
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }
    }

    public class GetAllPlacesHandler(IApplicationDbContext context) : IRequestHandler<GetAllPlacesQuery, List<PlaceDto>>
    {
        private readonly IApplicationDbContext _context = context;

        public async Task<List<PlaceDto>> Handle(GetAllPlacesQuery request, CancellationToken cancellationToken)
        {
            return await _context.PlacesInfo
                .Select(p => new PlaceDto
                {
                    PlaceId = p.PlaceId,
                    PlaceCountMembers = p.PlaceCountMembers,
                    PlaceHolder = p.PlaceHolder,
                    PlaceName = p.PlaceName
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
