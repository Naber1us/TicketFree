using MediatR;

namespace TicketFree.Features.Events.Delete
{
    public class CloseEventByIdCommand : IRequest<Event>
    {
        public Guid EventId { get; } = Guid.Empty;
    }
}
