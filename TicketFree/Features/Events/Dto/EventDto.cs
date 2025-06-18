using MediatR;
using TicketFree.Enums;
using TicketFree.Features.Events.Update;
using TicketFree.Validations;

namespace TicketFree.Features.Events.Dto
{
    public record EventDto
    {
        public Guid EventId { get; set; } = Guid.Empty;
        public Guid OrganizatorId { get; set; } = Guid.Empty;
        public Guid PlaceId { get; set; } = Guid.Empty;
        public Guid EventImage { get; set; } = Guid.Empty;
        public int EventCountTickets { get; set; } = 0;
        public DateTime EventStart { get; set; } = DateTime.Now;
        public DateTime EventEnd { get; set; } = DateTime.Now.AddDays(1);
        public string EventName { get; set; } = string.Empty;
        public string EventDescription { get; set; } = string.Empty;
        public EStatus EventStatus { get; set; } = EStatus.Open;
    }

    public record GetEventByIdQuery(Guid Id) : IRequest<EventDto?>;
    public record GetActivesEventsQuery : IRequest<List<EventDto>>;
    public record CloseEventByIdQuery(Guid Id) : IRequest<Result<EventDto>>;
    public record UpdateEventByIdQuery(UpdateEventByIdCommand Body, Guid Id) : IRequest<Result<Event>>;
}
