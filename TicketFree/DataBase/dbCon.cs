namespace TicketFree.DataBase
{
    public class dbCon
    {
        public static string Select()
        {
            var builder = WebApplication.CreateBuilder();
            string? ds = builder.Configuration.GetConnectionString("DatabaseConnection");
            return ds;
        }
        
    }
}
