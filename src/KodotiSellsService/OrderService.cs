using KodetiSellsModels;
using KodotiSellsCommon;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace KodotiSellsService
{
    public class OrderService
    {
         public List<Invoice> GetAll()
        {
            var result = new List<Invoice>();

            using (var context = new SqlConnection(Parameters.Connectionstring))
            {
                context.Open();

                var command = new SqlCommand("Select * From Invoices", context);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var invoice = new Invoice
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Iva = Convert.ToDecimal(reader["Iva"]),
                        SubTotal = Convert.ToDecimal(reader["SubTotal"]),
                        Total = Convert.ToDecimal(reader["Total"]),
                        ClientId = Convert.ToInt32(reader["ClientId"]),
                    };

                    result.Add(invoice);
                }

                //Set aditional properties
                foreach (var invoice in result)
                {
                    //Client
                    SetClient(invoice, context);
                }
            }

            return result;
        }

        private void SetClient(Invoice invoice, SqlConnection context)
        {
            throw new NotImplementedException();
        }
    }
}
