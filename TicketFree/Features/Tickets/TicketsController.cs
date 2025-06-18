using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketFree.Features.Tickets.Buy;
using TicketFree.Features.Tickets.Dto;

namespace TicketFree.Features.Tickets
{
    [ApiController]
    [Route("[controller]")]
    public class TicketsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        // GET: tickets/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketByIdQuery(Guid id)
        {
            var ticket = await _mediator.Send(new GetTicketByIdQuery(id));
            return ticket != null ? Ok(ticket) : NotFound();
        }

        [HttpGet("TicketsByEvent")]
        public async Task<IActionResult> GetTicketsByEventQuery(Guid id)
        {
            var ticket = await _mediator.Send(new GetTicketsByEventQuery(id));
            return ticket != null ? Ok(ticket) : NotFound();
        }

        [HttpGet("TicketsByUser")]
        public async Task<IActionResult> GetTicketsByUserQuery(Guid id)
        {
            var ticket = await _mediator.Send(new GetTicketsByUserQuery(id));
            return ticket != null ? Ok(ticket) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Ticket>> BuyTicket(BuyTicketCommand command)
        {
            var ticket = await _mediator.Send(command);
            return Ok(ticket);
        }
    }
}
