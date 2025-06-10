using Microsoft.AspNetCore.Mvc;
using MediatR;
using TicketFree.Features.Places.Dto;
using TicketFree.Features.Places.Create;

namespace TicketFree.Features.Places
{
    [ApiController]
    [Route("[controller]")]
    public class PlacesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAllPlaces()
        {
            var place = await _mediator.Send(new GetAllPlacesQuery());
            return Ok(place);
        }

        // GET: places/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlaceById(Guid id)
        {
            var place = await _mediator.Send(new GetPlaceByIdQuery(id));
            return place != null ? Ok(place) : NotFound();
        }

        [HttpPost(Name = "Places")]
        public async Task<ActionResult<Place>> CreateUser(CreatePlaceCommand command)
        {
            var place = await _mediator.Send(command);
            return Ok(place);
        }
    }
}
