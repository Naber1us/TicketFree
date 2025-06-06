using Microsoft.AspNetCore.Mvc;
using TicketFree.Requests;
using Microsoft.Data.SqlClient;
using Azure.Core;
using TicketFree.Enums;
using System.Data;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore;
using TicketFree.DataBase;

namespace TicketFree.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet(Name = "Users")]
    public IActionResult GetUser(
        [FromQuery] string? idUser = "0B8562DA-3C77-4351-8C13-4286FE512F52"
        )
    {
        try
        {
            string c = dbCon.Select();
            string query = $"SELECT * FROM dbo.usersInfo WHERE userId = '{idUser}'";
            string result = string.Empty;
            SqlConnection connection = new SqlConnection(c);
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
        catch
        {
            return BadRequest();
        }
        
    }

    [HttpPost(Name ="Users")]
    public IActionResult AddUser(
        [FromQuery] string? requestRole, 
        [FromQuery] string? userName
        )
    {
        try
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
            }
            ;
            Guid userToken = Guid.NewGuid();
            string c = dbCon.Select();
            string query = "INSERT INTO dbo.usersInfo VALUES ('" + userGuid + "', '" + userName + "', '" + userRole + "', '" + userToken + "')";
            SqlConnection connection = new SqlConnection(c);
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
        catch
        {
            return BadRequest();
        }
    }
}
