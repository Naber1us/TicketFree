using Microsoft.AspNetCore.Mvc;
using TicketFree.Requests;
using Microsoft.Data.SqlClient;
using Azure.Core;
using TicketFree.Enums;
using System.Data;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore;

namespace TicketFree.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet(Name = "Users")]
    public IActionResult GetUser([FromQuery] string? idUser)
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"; // Строка подключения SQL
        string query = "SELECT * FROM dbo.usersInfo WHERE userId = '" + idUser + "'";  // SQL-запрос
        string result = string.Empty;
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
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
        return new OkObjectResult(result);
    }

    [HttpPost(Name ="Users")]
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
        Guid userToken = Guid.NewGuid();
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"; // Строка подключения SQL
        string query = "INSERT INTO dbo.usersInfo VALUES ('"+ userGuid + "', '"+ userName + "', '"+ userRole + "', '"+ userToken + "')";  // SQL-запрос
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
        SqlDataReader reader = command.ExecuteReader();
        return Ok(new Users
        {
            userId = userGuid,
            userName = userName,
            userRole = userRole,
            userToken = userToken
        });
    }
}
