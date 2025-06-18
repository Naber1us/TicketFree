using MediatR;
using TicketFree.Validations;

namespace TicketFree.Features.Tickets.Buy
{

    public class BuyTicketCommand : IRequest<Result<Ticket>>
    {
        public Guid EventId { get; set; } = Guid.Empty;
        public Guid UserId { get; set; } = Guid.Empty;
    }


}
