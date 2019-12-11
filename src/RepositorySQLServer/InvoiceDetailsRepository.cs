using KodetiSellsModels;
using RepositoryInterface;
using System;
using System.Collections.Generic;

namespace RepositorySQLServer
{
    public class InvoiceDetailsRepository : Repository, IInvoiceDetailsRepository
    {
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
    }
}
