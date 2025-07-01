using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TicketFree.Features.Events.Dto;
using TicketFree.Interfaces;
using TicketFree.Validations;

namespace TicketFree.Features.Events.Update
{
    public class UpdateEventByIdCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<UpdateEventByIdQuery, Result<Event>>
    {
        public async Task<Result<Event>> Handle(UpdateEventByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Body.EventStart >= request.Body.EventEnd)
                {
                    return Result<Event>.Failure(
                        new Error("INVALID_DATES", "Дата начала должна быть раньше даты окончания"));
                }
                var eventEntity = await dbContext.EventsInfo
                    .FirstOrDefaultAsync(e => e.EventId == request.Id, cancellationToken);
                if (eventEntity == null)
                {
                    return Result<Event>.Failure(
                        new Error("NOT_FOUND", "Событие не найдено"));
                }

                if (request.Body.EventStart != null && eventEntity.EventStart != request.Body.EventStart)
                {
                    if (request.Body.EventStart < eventEntity.EventEnd)
                    {
                        eventEntity.EventStart = (DateTime)request.Body.EventStart;
                    }
                    else
                    {
                        return Result<Event>.Failure(new Error("INVALID_DATES", "Дата начала должна быть раньше даты окончания"));
                    }
                }

                if (request.Body.EventEnd != null && eventEntity.EventEnd != request.Body.EventEnd)
                {
                    if (eventEntity.EventStart < request.Body.EventEnd)
                        eventEntity.EventEnd = (DateTime)request.Body.EventEnd;
                    else
                        return Result<Event>.Failure(new Error("INVALID_DATES", "Дата начала должна быть раньше даты окончания"));
                }

                if (request.Body.EventName != null && eventEntity.EventName != request.Body.EventName)
                    eventEntity.EventName = request.Body.EventName;

                if (request.Body.EventDescription != null && eventEntity.EventDescription != request.Body.EventDescription)
                    eventEntity.EventDescription = request.Body.EventDescription;

                if (request.Body.EventImage != null && eventEntity.EventImage != request.Body.EventImage)
                    eventEntity.EventImage = (Guid)request.Body.EventImage;

                if (request.Body.PlaceId != null && eventEntity.PlaceId != request.Body.PlaceId)
                {
                    var place = await dbContext.PlacesInfo
                    .FirstOrDefaultAsync(p => p.PlaceId == request.Body.PlaceId, cancellationToken);

                    if (place == null)
                    {
                        return Result<Event>.Failure(
                            new Error("NOT_FOUND", "Указанное место не найдено"));
                    }

                    if (place.PlaceCountMembers <= eventEntity.EventCountTickets)
                    {
                        return Result<Event>.Failure(
                        new Error("CAPACITY_EXCEEDED", "В выбранном помещеннии недостаточно мест"));
                    }

                    eventEntity.PlaceId = (Guid)request.Body.PlaceId;
                }
                if (request.Body.EventCountTickets != null && eventEntity.EventCountTickets != request.Body.EventCountTickets)
                {
                    var place = await dbContext.PlacesInfo
                    .FirstOrDefaultAsync(p => p.PlaceId == request.Body.PlaceId, cancellationToken);

                    if (place == null)
                    {
                        return Result<Event>.Failure(
                            new Error("NOT_FOUND", "Указанное место не найдено"));
                    }

                    if (place.PlaceCountMembers <= request.Body.EventCountTickets)
                    {
                        return Result<Event>.Failure(
                        new Error("CAPACITY_EXCEEDED", "В выбранном помещеннии недостаточно мест"));
                    }

                    eventEntity.EventCountTickets = (int)request.Body.EventCountTickets;
                }
                if (request.Body.OrganizatorId != null && eventEntity.OrganizatorId != request.Body.OrganizatorId)
                {
                    var organizerExists = await dbContext.UsersInfo
                    .AnyAsync(u => u.UserId == request.Body.OrganizatorId, cancellationToken);

                    if (!organizerExists)
                    {
                        return Result<Event>.Failure(
                            new Error("NOT_FOUND", "Указанный организатор не найден"));
                    }

                    eventEntity.OrganizatorId = (Guid)request.Body.OrganizatorId;
                }

                await dbContext.SaveChangesAsync(cancellationToken);

                return Result<Event>.Success(eventEntity);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                return sqlEx.Number switch
                {
                    547 => Result<Event>.Failure(
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