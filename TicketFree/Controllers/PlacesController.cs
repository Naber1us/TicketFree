using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TicketFree.Enums;
using TicketFree.Requests;

namespace TicketFree.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlacesController : ControllerBase
    {
        [HttpGet(Name = "Places")]
        public IActionResult GetPlaces(
            [FromQuery] string? idPlace
            )
        {
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"; // Строка подключения SQL
                string query = "SELECT * FROM dbo.placesInfo WHERE placesId = '" + idPlace + "'";  // SQL-запрос
                string result = string.Empty;
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var Id = reader["placeId"];
                    var CountMembers = reader["placeCountMembers"];
                    var Holder = reader["placeHolder"];
                    var Name = reader["placeName"];
                    result += $"{{\n\t'placeId': '{Id}',\n\t'placeName':  '{Name}',\n\t'placeHolder': '{Holder}',\n\t'placeCountMembers': '{CountMembers}'\n}}";
                }
                return new OkObjectResult(result);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPost(Name = "Places")]
        public IActionResult AddPlaces(
            [FromQuery] string placeHolder, 
            [FromQuery] int placeCountMembers, 
            [FromQuery] string placeName
            )
        {
            try
            {
                Guid placeGuid = Guid.NewGuid();
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"; // Строка подключения SQL
                string query = $"INSERT INTO dbo.placesInfo VALUES ('{placeGuid}','{placeCountMembers}', '{placeHolder}', '{placeName}')";  // SQL-запрос
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                return Ok(new Places
                {
                    placeId = placeGuid,
                    placeCountMembers = placeCountMembers,
                    placeHolder = new Guid(placeHolder),
                    placeName = placeName
                });
            }
            catch
            {
                return BadRequest();
            }
           
        }
    }
}
