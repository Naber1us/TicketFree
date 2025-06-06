using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TicketFree.Requests;

namespace TicketFree.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {
        [HttpGet(Name = "Event")]
        public IActionResult GetEvent(
            [FromQuery] Guid? eventId, 
            [FromQuery] string? eventName, 
            [FromQuery] DateTime? eventStart, 
            [FromQuery] DateTime? eventEnd, 
            [FromQuery] Guid? organizatorId
            )
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            string query = "SELECT * FROM dbo.eventsInfo WHERE";
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

        [HttpPost(Name = "Event")]
        public IActionResult PostEvent(
            [FromQuery] Guid? eventImage,
            [FromQuery] string placeId = "95d2ad02-a82e-4606-ad02-124e1635dc97",
            [FromQuery] string eventName = "НГ",
            [FromQuery] int eventCountTickets = 2,
            [FromQuery] string eventStart = "2025-06-05 00:00",
            [FromQuery] string eventEnd = "2025-06-05 12:00",
            [FromQuery] string? eventDesription = "Новый год"
            )
        {
            DateTime eds = DateTime.Parse(eventStart);
            DateTime ede = DateTime.Parse(eventEnd);
            Guid eventId = Guid.NewGuid();
            Guid organizatorId = new Guid("c4fe727c-9ec9-4dd6-a3f0-edc193ed16b6");
            Guid placeIdGuid = new Guid(placeId);
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            string query = "INSERT INTO dbo.eventsInfo VALUES ('" + eventId + "', '" + organizatorId + "', '" + eventCountTickets + "', '" + eds + "', '" + ede + "', '" + eventName + "', '" + eventDesription + "', '" + eventImage + "', '" + placeIdGuid + "')";
            string result = string.Empty;

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            
            return Ok(new Events
            {
                eventId = eventId,
                eventName = eventName,
                eventStart = eds,
                eventEnd = ede,
                eventDescription = eventDesription,
                eventCountTickets = eventCountTickets,
                eventImage = eventImage,
                placeId = placeIdGuid,
                organizatorId = organizatorId
            });
        }
    }
}
