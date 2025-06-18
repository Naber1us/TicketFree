using MediatR;
using TicketFree.Enums;
using TicketFree.Validations;

namespace TicketFree.Features.Events.Update
{
    public class UpdateEventByIdCommand : IRequest<Result<Event>>
    {
        public Guid? OrganizatorId { get; set; } = null;
        public Guid? PlaceId { get; set; } = null;
        public Guid? EventImage { get; set; } = null;
        public int? EventCountTickets { get; set; } = null;
        public DateTime? EventStart { get; set; } = null;
        public DateTime? EventEnd { get; set; } = null;
        public string? EventName { get; set; } = null;
        public string? EventDescription { get; set; } = null;
        public EStatus? EventStatus { get; set; } = EStatus.Open;
    }
}
