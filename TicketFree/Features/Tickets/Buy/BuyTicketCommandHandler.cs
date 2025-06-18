using FluentValidation;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TicketFree.Enums;
using TicketFree.Features.Events.Dto;
using TicketFree.Features.Places;
using TicketFree.Features.Users.Create;
using TicketFree.Interfaces;
using TicketFree.Validations;

namespace TicketFree.Features.Tickets.Buy
{
    public class BuyTicketCommandHandler(IApplicationDbContext dbContext, IValidator<BuyTicketCommand> _validator) : IRequestHandler<BuyTicketCommand, Result<Ticket>>
    {
        public async Task<Result<Ticket>> Handle(BuyTicketCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            try
            {
                var eventEntity = await dbContext.EventsInfo
                    .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);
                if (eventEntity == null)
                {
                    return Result<Ticket>.Failure(
                        new Error("NOT_FOUND", "Событие не найдено"));
                }
                if(eventEntity.EventStatus == EStatus.Closed)
                {
                    return Result<Ticket>.Failure(
                        new Error("EVENT_CLOSED", "Продажа билетов прекращена"));
                }

                var userEntity = await dbContext.UsersInfo
                    .FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);
                
                if (userEntity == null)
                {
                    return Result<Ticket>.Failure(
                        new Error("NOT_FOUND", "Пользователь не найден"));
                }

                var ticket = new Ticket
                {
                    TicketId = Guid.NewGuid(),
                    EventId = request.EventId,
                    UserId = request.UserId
                };

                eventEntity.EventCountTickets--;
                if(eventEntity.EventCountTickets == 0)
                {
                    eventEntity.EventStatus = EStatus.Closed;
                }
                dbContext.TicketsInfo.Add(ticket);
                await dbContext.SaveChangesAsync(cancellationToken);

                return Result<Ticket>.Success(ticket);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                return Result<Ticket>.Failure(
                    new Error("DATABASE_ERROR", $"Ошибка базы данных: {sqlEx.Message}"));

            }
            catch (Exception ex)
            {
                return Result<Ticket>.Failure(
                    new Error("UNEXPECTED_ERROR", $"Неожиданная ошибка: {ex.Message}"));
            }

        }
    }

}
