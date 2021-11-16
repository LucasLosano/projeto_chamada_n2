using System.Data.SqlClient;

namespace Volare.DAO
{
    public class DbConnection
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = "Data Source=LAPTOP-OKNC9GE8\\SQLEXPRESS; Database=volareN2; integrated security=true";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}