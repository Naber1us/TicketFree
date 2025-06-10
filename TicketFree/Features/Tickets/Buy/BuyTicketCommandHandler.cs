using MediatR;
using TicketFree.Interfaces;

namespace TicketFree.Features.Tickets.Buy
{
    public class BuyTicketCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<BuyTicketCommand, Ticket>
    {
        public async Task<Ticket> Handle(BuyTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = new Ticket
            {
                TicketId = Guid.NewGuid(),
                EventId = request.EventId,
                UserId = request.UserId
            };

            dbContext.TicketsInfo.Add(ticket);
            await dbContext.SaveChangesAsync(cancellationToken);

            return ticket;

        }
    }

}
