using KodetiSellsModels;
using RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RepositorySQLServer
{
    public class InvoiceDetailsRepository : Repository, IInvoiceDetailsRepository
    {
       

        public InvoiceDetailsRepository(SqlConnection context, SqlTransaction transaction)
        {
            _contex = context;
           _transaction = transaction;
        }

        public    void Create(IEnumerable<InvoiceDetail> invoiceDetails, int invoiceId)
        {
            foreach (var details in invoiceDetails)
            {
                var sqlQuery = "Insert Into InvoiceDetail(InvoiceId,ProductId,Quantity,Price,Iva,SubTotal,Total) Values (@InvoiceId,@ProductId,@Quantity,@Price,@Iva,@SubTotal,@Total)";
                var cmd = CreateCommnad(sqlQuery);

                cmd.Parameters.AddWithValue("@InvoiceId",invoiceId);
                cmd.Parameters.AddWithValue("@ProductId", details.ProductId);
                cmd.Parameters.AddWithValue("@Quantity", details.Quantity);
                cmd.Parameters.AddWithValue("@Price", details.Price);
                cmd.Parameters.AddWithValue("@Iva", details.Iva);
                cmd.Parameters.AddWithValue("@SubTotal", details.SubTotal);
                cmd.Parameters.AddWithValue("@Total", details.Total);

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerator<InvoiceDetail> GetAll()
        {
            throw new NotImplementedException();
        }

        public InvoiceDetail GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InvoiceDetail> GetAllInvoicedetailsById(int Id)
        {
            var result = new List<InvoiceDetail>();

            var cmd = CreateCommnad("Select * from InvoiceDetail with(NOLOCK) InvoiceId = @InvoiceId");
            cmd.Parameters.AddWithValue("@InvoiceId", Id);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new InvoiceDetail
                    {
                        Id = Convert.ToInt32(reader["@Id"]),
                        ProductId = Convert.ToInt32(reader["@ProductId"]),
                        Quantity = Convert.ToInt32(reader["@Quantity"]),
                        Iva = Convert.ToDecimal(reader["@Iva"]),
                        SubTotal = Convert.ToDecimal(reader["@SubTotal"]),
                        Total = Convert.ToInt32(reader["@Total"]),

                    });
                }
            }


            return result;
        }

        public void Update(InvoiceViewModel invoiceViewModel)
        {
            var sqlQuery = "Update Invoices Set ClientId = @ClientId, Iva = @Iva, SubTotal = @SubTotal, Total = @Total Where Id = @Id";
            var cmd = CreateCommnad(sqlQuery);       

            cmd.Parameters.AddWithValue("@ClientId", invoiceViewModel.ClientId);
            cmd.Parameters.AddWithValue("@Iva", invoiceViewModel.Iva);
            cmd.Parameters.AddWithValue("@SubTotal", invoiceViewModel.SubTotal);
            cmd.Parameters.AddWithValue("@Total", invoiceViewModel.Total);
            cmd.Parameters.AddWithValue("@Id", invoiceViewModel.Id);

            cmd.ExecuteNonQuery();
        }

        public void RemoveInvoiceIdById(int id)
        {
            var query = "Delete from InvoiceDetail where InvoiceId = @InvoiceId";

            var cmd = CreateCommnad(query);
            cmd.Parameters.AddWithValue("@InvoiceId", id);

            cmd.ExecuteNonQuery();
        }
    }
}
