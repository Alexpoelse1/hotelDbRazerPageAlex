namespace hotelDbRazerPage.Services
{
    public class Secret
    {
        private static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=hotelDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
         public static string ConnectionString
        {
            get { return connectionString; }
        }

    }
}
