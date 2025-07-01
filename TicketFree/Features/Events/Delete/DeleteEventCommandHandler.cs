using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketFree.Enums;
using TicketFree.Features.Events.Dto;
using TicketFree.Interfaces;
using TicketFree.Validations;

namespace TicketFree.Features.Events.Delete
{
    public class CloseEventByIdQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<CloseEventByIdQuery, Result<EventDto>>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<EventDto>> Handle(CloseEventByIdQuery request, CancellationToken cancellationToken)
        {
            var eventEntity = await _context.EventsInfo
                                        .FirstOrDefaultAsync(e => e.EventId == request.Id, cancellationToken);
            if (eventEntity == null)
            {
                return Result<EventDto>.Failure(
                    new Error("NOT_FOUND", "Событие не найдено"));
            }

            if (eventEntity.EventStatus == EStatus.Closed)
            {
                return Result<EventDto>.Failure(
                    new Error("INVALID_OPERATION", "Событие уже закрыто"));
            }
            eventEntity.EventStatus = EStatus.Closed;
            try
            {
                await _context.SaveChangesAsync(cancellationToken);

                var result = _mapper.Map<EventDto>(eventEntity);
                return Result<EventDto>.Success(result);
            }
            catch (DbUpdateException ex)
            {
                return Result<EventDto>.Failure(
                    new Error("DATABASE_ERROR", $"Ошибка при сохранении: {ex.Message}"));
            }
        }
    }
}
