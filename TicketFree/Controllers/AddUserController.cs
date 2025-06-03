using Microsoft.AspNetCore.Mvc;
using TicketFree.Requests;
using Microsoft.Data.SqlClient;
using Azure.Core;
using TicketFree.Enums;

namespace TicketFree.Controllers;

[ApiController]
[Route("[controller]")]
public class AddUserController : ControllerBase
{
    [HttpPost(Name ="GetAddUser")]
    public IActionResult AddUser([FromQuery] string? requestRole, [FromQuery] string? userName)
    {
        Guid userGuid = Guid.NewGuid();
        ERoles userRole;
        switch (requestRole)
        {
            case "Admin":
                {
                    userRole = ERoles.Admin;
                    break;
                }
            case "PlaceHolder":
                {
                    userRole = ERoles.PlaceHolder;
                    break;
                }
            case "Organizator":
                {
                    userRole = ERoles.Organizator;
                    break;
                }
            case "Guest":
                {
                    userRole = ERoles.Guest;
                    break;
                }
            default:
                {
                    userRole = ERoles.Guest;
                    break;
                }
        };
        Guid token = Guid.NewGuid();
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"; // Замените на свою строку подключения
        string query = "INSERT INTO dbo.userInfo VALUES ('"+ userGuid + "', '"+ userName + "', '"+ userRole + "', '"+ token + "')";  // Замените на свой SQL-запрос
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
        SqlDataReader reader = command.ExecuteReader();
        return Ok(new AddUser
        {
            UserId = userGuid,
            Name = userName,
            Role = userRole,
            Token = token
        });
    }

}
