using System.Data;
using System.Data.SqlClient;

namespace Volare.DAO
{
    public static class HelperDAO
    {
        public static void ExecuteProcedure(string procudereName, SqlParameter[] parameters)
        {
            using(SqlConnection connection = DbConnection.GetConnection())
            {
                using(SqlCommand command = new SqlCommand(procudereName, connection))
                {
                    if(parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public static DataTable ExecuteProcedureSelect(string procedureName, SqlParameter[] parameters)
        {
            using(SqlConnection connection = DbConnection.GetConnection())
            {
                using(SqlDataAdapter adapter = new SqlDataAdapter(procedureName, connection))
                {
                    if(parameters != null)
                    {
                        adapter.SelectCommand.Parameters.AddRange(parameters);
                    }
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    connection.Close();

                    return table;
                }
            }
        }
    }
}