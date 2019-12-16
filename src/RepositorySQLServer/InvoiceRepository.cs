using KodetiSellsModels;
using RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RepositorySQLServer
{
    public class InvoiceRepository : Repository, IInvoiceRepository
    {
        
        public InvoiceRepository(SqlConnection context,  SqlTransaction  transaction)
        {
            _contex = context;
            _transaction = transaction;
        }
        public void Create(Invoice invoice)
        {
            var query = "Insert into Invoices(ClientId, Iva, SubTotal, Total) output INSERTED.ID " +
                "        values (@ClientId, @Iva, @SubTotal, @Total)";
            var cmd = CreateCommnad(query);

            cmd.Parameters.AddWithValue("@ClientId", invoice.ClientId);
            cmd.Parameters.AddWithValue("@Iva", invoice.Iva);
            cmd.Parameters.AddWithValue("@SubTotal", invoice.SubTotal);
            cmd.Parameters.AddWithValue("@Total", invoice.Total);

            invoice.Id = Convert.ToInt32(cmd.ExecuteScalar());
        }

        public IEnumerator<Invoice> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Invoice GetById(int id)
        {
            var result = new Invoice();

            var cmd = CreateCommnad("Select * From Invoices Where Id = @id");
            cmd.Parameters.AddWithValue("@Id", id);

            using (var reader = cmd.ExecuteReader())
            {
                reader.Read();

                result.Id = Convert.ToInt32(reader["@Id"]);
                result.Iva = Convert.ToDecimal(reader["@Iva"]);
                result.SubTotal= Convert.ToDecimal(reader["@SubTotal"]);
                result.Total = Convert.ToInt32(reader["@Total"]);
                result.ClientId = Convert.ToInt32(reader["@clientId"]);
            }

            return result;
        }

        public void Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Invoice t)
        {
            throw new System.NotImplementedException();
        }
    }
}
