using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace TicketFree.Features.Tickets
{
    [ApiController]
    [Route("[controller]")]
    public class TicketsController: ControllerBase
    {
        [HttpGet(Name = "Tickets")]
        public IActionResult GetListTickets(
            [FromQuery] Guid? ticketId, 
            [FromQuery] Guid? userId
            )
        {

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            string query = $"SELECT * FROM dbo.ticketsInfo WHERE ticketId = '{ticketId}' OR userId = '{userId}'";
            string result = $"{{\n";
            SqlConnection connection = new(connectionString);
            connection.Open();
            SqlCommand command = new(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var Id = reader["eventId"];
                var Name = reader["eventName"];
                var Start = reader["eventStart"];
                var End = reader["eventEnd"];
                var Description = reader["eventDescription"];
                var Image = reader["eventImage"];
                var Place = reader["placeId"];
                var Organizator = reader["organizatorId"];
                result += $"\t{{\n\t\t'eventId': '{Id}',\n\t\t'eventName':  '{Name}',\n\t\t'eventStart': '{Start}',\n\t\t'eventEnd': '{End}',\n\t\t'eventDesription': '{Description}',\n\t\t'eventImage': '{Image}',\n\t\t'placeId': '{Place}',\n\t\t'organizatorId': '{Organizator}'\n\t}}";
            }
            result += $"\n}}";
            return new OkObjectResult(result);
        }

        [HttpPost(Name = "Tickets")]
        public IActionResult AddTickets(
            [FromQuery] string eventId = "1b863944-780a-4e7d-8f89-b280fb65b53b",
            [FromQuery] string userId = "0B8562DA-3C77-4351-8C13-4286FE512F52"

            )
        {
            Guid ticketId = Guid.NewGuid();
            Guid ei = new(eventId);
            Guid ui = new(userId);
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            string query = $"INSERT INTO dbo.ticketsInfo VALUES ('{ticketId}', '{ei}', '{ui}')";
            
            SqlConnection connection = new(connectionString);
            connection.Open();
            SqlCommand command = new(query, connection);
            _ = command.ExecuteReader();
            return Ok(new Tickets
            {
                TicketId = ticketId,
                EventId = ei,
                UserId = ui
            });
        }
    }
}
