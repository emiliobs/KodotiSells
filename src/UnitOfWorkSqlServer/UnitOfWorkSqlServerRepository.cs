using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using UinitOfworkInterface;

namespace UnitOfWorkSqlServer
{
    public class UnitOfWorkSqlServerRepository : IUnitOfWorkRepository
    {
        public UnitOfWorkSqlServerRepository(SqlConnection context, SqlTransaction transaction)
        {

        }
    }      
}
