using System.Data.SqlClient;

namespace RepositorySQLServer
{
    public abstract class Repository
    {
        protected SqlConnection _contex;
        protected SqlTransaction _transaction;

        protected SqlCommand CreateCommnad(string query)
        {
            return new SqlCommand(query, _contex, _transaction);
        }
    }
}
