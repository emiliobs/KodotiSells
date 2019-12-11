using RepositoryInterface;
using RepositorySQLServer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using UinitOfworkInterface;

namespace UnitOfWorkSqlServer
{
    public class UnitOfWorkSqlServerRepository : IUnitOfWorkRepository
    {
        public IInvoiceRepository InvoiceRepository { get; }

        public IProductRepository ProductRepository { get; }

        public IInvoiceDetailsRepository InvoiceDetailsRepository { get; }

        public IClientRepository ClientRepository { get; }

        public UnitOfWorkSqlServerRepository(SqlConnection context, SqlTransaction transaction)
        {
            InvoiceRepository = new InvoiceRepository(context, transaction);
            ProductRepository = new ProducRepository();
            ClientRepository = new ClientRepository();
            InvoiceDetailsRepository = new InvoiceDetailsRepository();

        }

    }      
}
