using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketFree.Features.Events.Create;
using TicketFree.Features.Events.Dto;
using TicketFree.Features.Events.Update;

namespace TicketFree.Features.Events
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetActiveEvents()
        {
            var @event = await _mediator.Send(new GetActivesEventsQuery());
            return Ok(@event);
        }

        // GET: event/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(Guid id)
        {
            var @event = await _mediator.Send(new GetEventByIdQuery(id));
            return @event != null ? Ok(@event) : NotFound();
        }

        [HttpPost(Name = "Events")]
        public async Task<ActionResult<Event>> CreateEvent(CreateEventCommand command)
        {
            var @event = await _mediator.Send(command);
            return Ok(@event);
        }

        // DELETE: event/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Event>> DeleteEventById(Guid id)
        {
            var @event = await _mediator.Send(new CloseEventByIdQuery(id));
            return @event != null ? Ok(@event) : NotFound();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Event>> UpdateEventById(UpdateEventByIdCommand command, Guid id)
        {
            var @event = await _mediator.Send(new UpdateEventByIdQuery(command, id));
            return @event != null ? Ok(@event) : NotFound();
        }
    }
}
