using MediatR;

namespace TicketFree.Features.Tickets.Buy
{
    
        public class BuyTicketCommand : IRequest<Ticket>
        {
            public Guid EventId { get; set; } = Guid.Empty;
            public Guid UserId { get; set; } = Guid.Empty;
        }


}
