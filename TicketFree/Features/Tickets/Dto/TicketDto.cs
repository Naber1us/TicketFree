using MediatR;

namespace TicketFree.Features.Tickets.Dto
{
    public record TicketDto
    {
        public Guid TicketId { get; set; } = Guid.Empty;
        public Guid UserId { get; set; } = Guid.Empty;
        public Guid EventId { get; set; } = Guid.Empty;
    }

    public record GetTicketByIdQuery(Guid Id) : IRequest<TicketDto?>;
    public record GetTicketsByUserQuery(Guid Id) : IRequest<List<TicketDto>>;
    public record GetTicketsByEventQuery(Guid Id) : IRequest<List<TicketDto>>;
}
