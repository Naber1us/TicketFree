using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TicketFree.Interfaces;
using TicketFree.Validations;

namespace TicketFree.Features.Events.Create
{
    public class CreateEventCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<CreateEventCommand, Result<Event>>
    {
        public async Task<Result<Event>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.EventStart >= request.EventEnd)
                {
                    return Result<Event>.Failure(
                        new Error("INVALID_DATES", "Дата начала должна быть раньше даты окончания"));
                }

                var place = await dbContext.PlacesInfo
                    .FirstOrDefaultAsync(p => p.PlaceId == request.PlaceId, cancellationToken);

                if (place == null)
                {
                    return Result<Event>.Failure(
                        new Error("NOT_FOUND", "Указанное место не найдено"));
                }

                if (place.PlaceCountMembers <= request.EventCountTickets)
                {
                    return Result<Event>.Failure(
                        new Error("CAPACITY_EXCEEDED",
                            $"Количество билетов ({request.EventCountTickets}) " +
                            $"превышает вместимость помещения ({place.PlaceCountMembers})"));
                }

                var organizerExists = await dbContext.UsersInfo
                    .AnyAsync(u => u.UserId == request.OrganizatorId, cancellationToken);

                if (!organizerExists)
                {
                    return Result<Event>.Failure(
                        new Error("NOT_FOUND", "Указанный организатор не найден"));
                }

                var @event = new Event
                {
                    EventId = Guid.NewGuid(),
                    EventName = request.EventName,
                    EventDescription = request.EventDescription,
                    EventImage = request.EventImage,
                    EventStart = request.EventStart,
                    EventEnd = request.EventEnd,
                    PlaceId = request.PlaceId,
                    EventCountTickets = request.EventCountTickets,
                    OrganizatorId = request.OrganizatorId,
                    EventStatus = request.EventStatus
                };

                dbContext.EventsInfo.Add(@event);
                await dbContext.SaveChangesAsync(cancellationToken);

                return Result<Event>.Success(@event);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                return sqlEx.Number switch
                {
                    547 => Result<Event>.Failure( // FK violation
                        new Error("RELATIONSHIP_ERROR",
                            "Ошибка связи: проверьте существование связанных сущностей")),
                    _ => Result<Event>.Failure(
                        new Error("DATABASE_ERROR", $"Ошибка базы данных: {sqlEx.Message}"))
                };
            }
            catch (Exception ex)
            {
                return Result<Event>.Failure(
                    new Error("UNEXPECTED_ERROR", $"Неожиданная ошибка: {ex.Message}"));
            }
        }
    }
}
