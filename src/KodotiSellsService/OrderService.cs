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
         public List<InvoiceViewModel> GetAll()
        {
            var result = new List<InvoiceViewModel>();

            using (var context = new SqlConnection(Parameters.Connectionstring))
            {
                context.Open();

                var command = new SqlCommand("Select * From Invoices", context);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var invoiceViewModel = new InvoiceViewModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Iva = Convert.ToDecimal(reader["Iva"]),
                            SubTotal = Convert.ToDecimal(reader["SubTotal"]),
                            Total = Convert.ToDecimal(reader["Total"]),
                            ClientId = Convert.ToInt32(reader["ClientId"]),
                        };

                        result.Add(invoiceViewModel);
                    }
                }    

                //Set aditional properties
                foreach (var invoiceViewModel in result)
                {
                    //Client
                    SetClient(invoiceViewModel, context);

                    //InvoiceDetail
                    SetInvoiceDetails(invoiceViewModel, context);
                }
            }

            return result;
        }

        private void SetClient(InvoiceViewModel invoiceViewModel, SqlConnection context)
        {
            var command = new SqlCommand("Select * from Clients where Id = @ClientdId", context);
            command.Parameters.AddWithValue("@ClientdId", invoiceViewModel.ClientId);
            

            using (var reader = command.ExecuteReader())
            {
                reader.Read();

                invoiceViewModel.Client = new Client
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                };
            };
            

           // invoice.Client = client;
        }

        private void SetInvoiceDetails(InvoiceViewModel invoiceViewModel, SqlConnection context)
        {
            var command = new SqlCommand("Select * from InvoiceDetail where InvoiceId = @InvoiceId", context);
            command.Parameters.AddWithValue("@InvoiceId", invoiceViewModel.Id);


            using (var reader = command.ExecuteReader())
            {

                //aqui itero por que puede haver muxhos registro InvoiceDetails Realcionados
                while (reader.Read())
                {
                    invoiceViewModel.InvoiceDetails.Add(new InvoiceDetail 
                    {
                         Id = Convert.ToInt32(reader["Id"]),
                         ProductId = Convert.ToInt32(reader["ProductId"]),
                         InvoiceId = Convert.ToInt32(reader["InvoiceId"]),
                         Quantity = Convert.ToInt32(reader["Quantity"]),
                         Iva = Convert.ToDecimal(reader["Iva"]),
                         SubTotal = Convert.ToDecimal(reader["SubTotal"]),
                         Total = Convert.ToDecimal(reader["Total"]),
                         Price = Convert.ToDecimal(reader["Price"]),
                         Invoice = invoiceViewModel,
                    });
                }

            };

            foreach (var invoiceDedail in invoiceViewModel.InvoiceDetails)
            {
                //Products:
                SetProductEnvoiceDetail(invoiceDedail, context);
            }

        }

        private void SetProductEnvoiceDetail(InvoiceDetail invoiceDedail, SqlConnection context)
        {
            try
            {
                var command = new SqlCommand("Select * from Products where Id = @ProductId", context);
                command.Parameters.AddWithValue("@ProductId", invoiceDedail.ProductId);

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();

                    invoiceDedail.Product = new Product
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                    };
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
