using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketFree.Features.Events;
using TicketFree.Features.Tickets.Buy;
using TicketFree.Features.Tickets;
using TicketFree.Interfaces;
using TicketFree.Validations;
using Microsoft.Data.SqlClient;

namespace TicketFree.Features.Events.Update
{
    public class UpdateEventByIdCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<UpdateEventByIdCommand, Result<Event>>
    {
        public async Task<Result<Event>> Handle(UpdateEventByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.EventStart >= request.EventEnd)
                {
                    return Result<Event>.Failure(
                        new Error("INVALID_DATES", "Дата начала должна быть раньше даты окончания"));
                }
                var eventEntity = await dbContext.EventsInfo
                    .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);
                if(eventEntity == null)
                    return Result<Event>.Failure(
                        new Error("NOT_FOUND", "Событие не найдено"));

                if (request.EventStart != null && eventEntity.EventStart != request.EventStart)
                    if(request.EventStart < eventEntity.EventEnd)
                        eventEntity.EventStart = (DateTime)request.EventStart;
                    else
                        return Result<Event>.Failure(
                            new Error("INVALID_DATES", "Дата начала должна быть раньше даты окончания"));

                if (request.EventEnd != null && eventEntity.EventEnd != request.EventEnd)
                    if (eventEntity.EventStart < request.EventEnd)
                        eventEntity.EventEnd = (DateTime)request.EventEnd;
                    else
                        return Result<Event>.Failure(
                            new Error("INVALID_DATES", "Дата начала должна быть раньше даты окончания"));

                if (request.EventName != null && eventEntity.EventName != request.EventName)
                    eventEntity.EventName = request.EventName;

                if (request.EventDescription != null && eventEntity.EventDescription != request.EventDescription)
                    eventEntity.EventDescription = request.EventDescription;

                if (request.EventImage != null && eventEntity.EventImage != request.EventImage)
                    eventEntity.EventImage = (Guid)request.EventImage;
                
                if(request.PlaceId != null && eventEntity.PlaceId != request.PlaceId)
                {
                    var place = await dbContext.PlacesInfo
                    .FirstOrDefaultAsync(p => p.PlaceId == request.PlaceId, cancellationToken);

                    if (place == null)
                    {
                        return Result<Event>.Failure(
                            new Error("NOT_FOUND", "Указанное место не найдено"));
                    }

                    if (place.PlaceCountMembers <= eventEntity.EventCountTickets)
                    {
                        return Result<Event>.Failure(
                        new Error("CAPACITY_EXCEEDED", $"В выбранном помещеннии недостаточно мест"));
                    }

                    eventEntity.PlaceId = (Guid)request.PlaceId;
                }
                if(request.EventCountTickets != null && eventEntity.EventCountTickets != request.EventCountTickets)
                {
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
                        new Error("CAPACITY_EXCEEDED", $"В выбранном помещеннии недостаточно мест"));
                    }

                    eventEntity.EventCountTickets = (int)request.EventCountTickets;
                }
                if (request.OrganizatorId != null && eventEntity.OrganizatorId != request.OrganizatorId)
                {
                    var organizerExists = await dbContext.UsersInfo
                    .AnyAsync(u => u.UserId == request.OrganizatorId, cancellationToken);

                    if (!organizerExists)
                    {
                        return Result<Event>.Failure(
                            new Error("NOT_FOUND", "Указанный организатор не найден"));
                    }

                    eventEntity.OrganizatorId = (Guid)request.OrganizatorId;
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