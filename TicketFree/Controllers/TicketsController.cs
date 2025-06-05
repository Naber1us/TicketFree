using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace TicketFree.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketsController: ControllerBase
    {
        [HttpGet("GetTickets")]
        public IActionResult GetListTickets([FromQuery] Guid ticketId)
        {

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            string query = $"SELECT * FROM dbo.ticketsInfo WHERE ticketId = {ticketId}";
            var subquery = new List<string>();
            if (eventId != null)
                subquery.Add($" eventId = '{eventId}' ");
            if (eventName != null)
                subquery.Add($" eventName = '{eventName}' ");
            if (eventStart != null && eventEnd != null)
                subquery.Add($" (eventStart >= '{eventStart}' AND eventEnd <= '{eventEnd}') ");
            if (organizatorId != null)
                subquery.Add($" organizatorId = '{organizatorId}'");
            query += String.Join(" AND ", subquery.ToArray());
            string result = $"{{\n";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
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
                var organizator = reader["organizatorId"];
                result += $"\t{{\n\t\t'eventId': '{Id}',\n\t\t'eventName':  '{Name}',\n\t\t'eventStart': '{Start}',\n\t\t'eventEnd': '{End}',\n\t\t'eventDesription': '{Description}',\n\t\t'eventImage': '{Image}',\n\t\t'placeId': '{Place}',\n\t\t'organizatorId': '{Place}'\n\t}}";
            }
            result += $"\n}}";
            return new OkObjectResult(result);
        }
    }
}
