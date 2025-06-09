using Microsoft.AspNetCore.Mvc;
using MediatR;
using TicketFree.Features.Users.Create;

namespace TicketFree.Features.Users;

[ApiController]
[Route("[controller]")]
public class UserController (IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id}")]
    public IActionResult GetUser(Guid id)
    {
        /*try
        {
            string? c = DataBaseConnection.Select();
            string query = $"SELECT * FROM dbo.usersInfo WHERE userId = '{id}'";
            string result = string.Empty;
            SqlConnection connection = new(c);
            connection.Open();
            SqlCommand command = new(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var Id = reader["userId"];
                var Name = reader["userName"];
                var Role = string.Empty;
                switch (reader["userRole"])
                {
                    case "0":
                        {
                            Role = "Admin";
                            break;
                        }
                    case "1":
                        {
                            Role = "PlaceHolder";
                            break;
                        }
                    case "2":
                        {
                            Role = "Organizator";
                            break;
                        }
                    default:
                        {
                            Role = "Guest";
                            break;
                        }
                }
                var Token = reader["userToken"];
                result += $"{{\n\t'userId': '{Id}',\n\t'userName':  '{Name}',\n\t'userRole': '{Role}',\n\t'userToken': '{Token}'    \n}}";
            }
            if(result == string.Empty)
            {
                result = $"{{\n\t'response': 'Нет пользователей с таким id'\n}}";
            }
            
            return new OkObjectResult(result);
        }
        catch
        {
            return BadRequest();
        }*/
        {
            return Ok();
        }
        
    }
    [HttpPost(Name = "Users")]
    public async Task<ActionResult<User>> CreateUser(CreateUserCommand command)
    {
        var userId = await _mediator.Send(command);
        return Ok(userId);
    }
}
