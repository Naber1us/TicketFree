using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketFree.Features.Users.Create;
using TicketFree.Features.Users.Dto;

namespace TicketFree.Features.Users;

[ApiController]
[Route("[controller]")]
public class UserController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _mediator.Send(new GetAllUsersQuery());
        return Ok(users);
    }

    // GET: users/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));
        return user != null ? Ok(user) : NotFound();
    }

    [HttpPost(Name = "Users")]
    public async Task<ActionResult<User>> CreateUser(CreateUserCommand command)
    {
        var User = await _mediator.Send(command);
        return Ok(User);
    }
}
