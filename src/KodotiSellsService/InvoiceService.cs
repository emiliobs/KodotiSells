using KodetiSellsModels;
using KodotiSellsCommon;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using UinitOfworkInterface;

namespace KodotiSellsService
{
    public class InvoiceService
    {
        private readonly IUnitOfwork _unitOfwork;

        public InvoiceService(IUnitOfwork unitOfwork)
        {
            _unitOfwork = unitOfwork;
        }

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

        public void Create(InvoiceViewModel invoiceViewModel)
        {
            PreparedOrder(invoiceViewModel);

            using (var context = _unitOfwork.Create())
            {
                //Header:
                context.Repository.InvoiceRepository.Create(invoiceViewModel);

                //Details
                context.Repository.InvoiceDetailsRepository.Create(invoiceViewModel.InvoiceDetails, invoiceViewModel.Id);

                context.SaveChange();

            }

            using (var transaction = new TransactionScope() )
            {
                using (var db = new SqlConnection(Parameters.Connectionstring))
                {
                    db.Open();

                    //Insert Header
                    //AddHeader(invoiceViewModel, db);

                    //InvoiceDetail
                    //AddInvoiceDetail(invoiceViewModel, db);
                }



                transaction.Complete();
            }
        }

        public void Update(InvoiceViewModel invoiceViewModel)
        {
            PreparedOrder(invoiceViewModel);

            using (var context = _unitOfwork.Create())
            {
                //Header
                context.Repository.InvoiceDetailsRepository.Update(invoiceViewModel);

                //Details:
                context.Repository.InvoiceDetailsRepository.RemoveInvoiceIdById(invoiceViewModel.Id);
                context.Repository.InvoiceDetailsRepository.Create(invoiceViewModel.InvoiceDetails, invoiceViewModel.Id);

                //Confirm Changes
                context.SaveChange();
            }

           


            //using (var transaction = new TransactionScope())
            //{
            //    using (var context = new SqlConnection(Parameters.Connectionstring))
            //    {
            //        context.Open();

            //        //Header
            //        UpdateHeader(invoiceViewModel, context);

            //        //Remove
            //        RemoverInvoiceDetail(invoiceViewModel.Id, context);

            //        //Detail
            //        //AddInvoiceDetail(invoiceViewModel, context);


            //    }

            //    transaction.Complete();
            //}
        }

        public void Delete(int id)
        {
            using (var context = _unitOfwork.Create())
            {
                context.Repository.InvoiceRepository.Remove(id);

                //Confirm Cahnge:
                context.SaveChange();
            }       

            //using (var context = new SqlConnection(Parameters.Connectionstring))
            //{
            //    context.Open();

            //    var cmd = new SqlCommand("Delete  From Invoices where Id = @Id", context);
            //    cmd.Parameters.AddWithValue("@Id", id);

            //    cmd.ExecuteNonQuery();
            //}
        }

        private void RemoverInvoiceDetail(int invoiceId, SqlConnection sqlConnection)
        {
            var query = "Delete from InvoiceDetail where InvoiceId = @InvoiceId";

            var cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@InvoiceId", invoiceId);

            cmd.ExecuteNonQuery();
        }

        private void UpdateHeader(InvoiceViewModel invoiceViewModel, SqlConnection sqlConnection)
        {
            var sqlQuery = "Update Invoices Set ClientId = @ClientId, Iva = @Iva, SubTotal = @SubTotal, Total = @Total Where Id = @Id";
            var cmd = new SqlCommand(sqlQuery, sqlConnection);

            cmd.Parameters.AddWithValue("@ClientId", invoiceViewModel.ClientId);
            cmd.Parameters.AddWithValue("@Iva", invoiceViewModel.Iva);
            cmd.Parameters.AddWithValue("@SubTotal", invoiceViewModel.SubTotal);
            cmd.Parameters.AddWithValue("@Total", invoiceViewModel.Total);
            cmd.Parameters.AddWithValue("@Id", invoiceViewModel.Id);

            cmd.ExecuteNonQuery();   
        }


        //private void AddHeader(InvoiceViewModel invoiceViewModel, SqlConnection sqlConnection)
        //{
        //    var sqlQuery = "Insert Into Invoices(ClientId, Iva, SubTotal, Total) output INSERTED.ID Values (@ClientId, @Iva, @SubTotal, @Total)";
        //    var cmd = new SqlCommand(sqlQuery, sqlConnection);

        //    cmd.Parameters.AddWithValue("@ClientId", invoiceViewModel.ClientId);
        //    cmd.Parameters.AddWithValue("@Iva", invoiceViewModel.Iva);
        //    cmd.Parameters.AddWithValue("@SubTotal", invoiceViewModel.SubTotal);
        //    cmd.Parameters.AddWithValue("@Total", invoiceViewModel.Total);

        //    invoiceViewModel.Id = Convert.ToInt32(cmd.ExecuteScalar());
        //}

        //private void AddInvoiceDetail(InvoiceViewModel invoiceViewModel, SqlConnection sqlConnection)
        //{
        //    foreach (var details in invoiceViewModel.InvoiceDetails)
        //    {
        //        var sqlQuery = "Insert Into InvoiceDetail(InvoiceId,ProductId,Quantity,Price,Iva,SubTotal,Total) " +
        //                        "Values (@InvoiceId,@ProductId,@Quantity,@Price,@Iva,@SubTotal,@Total)";
        //        var cmd = new SqlCommand(sqlQuery, sqlConnection);

        //        cmd.Parameters.AddWithValue("@InvoiceId", invoiceViewModel.Id);
        //        cmd.Parameters.AddWithValue("@ProductId", details.ProductId);
        //        cmd.Parameters.AddWithValue("@Quantity", details.Quantity);
        //        cmd.Parameters.AddWithValue("@Price", details.Price);
        //        cmd.Parameters.AddWithValue("@Iva", details.Iva);
        //        cmd.Parameters.AddWithValue("@SubTotal", details.SubTotal);
        //        cmd.Parameters.AddWithValue("@Total", details.Total);

        //        cmd.ExecuteNonQuery();
        //    }
        //}

        private void PreparedOrder(InvoiceViewModel invoiceViewModel)
        {

            //cualculos del invoiceDetails
            foreach (var detail in invoiceViewModel.InvoiceDetails)
            {
                detail.Total = detail.Quantity * detail.Price;
                detail.Iva = detail.Total * Parameters.IvaRate;
                detail.SubTotal = detail.Total - detail.Iva;
            }

            //aqui sumo todos los calculos de los totales:
            invoiceViewModel.Total = invoiceViewModel.InvoiceDetails.Sum(t => t.Total);
            invoiceViewModel.Iva = invoiceViewModel.InvoiceDetails.Sum(i => i.Iva);
            invoiceViewModel.SubTotal = invoiceViewModel.InvoiceDetails.Sum(s => s.SubTotal);

        }

        public InvoiceViewModel GetInvoiceById(int id)
        {
            var invoiceViewModel = new InvoiceViewModel();

            try
            {
                using (var context = new SqlConnection(Parameters.Connectionstring))
                {
                    context.Open();

                    var cmd = new SqlCommand("select * from Invoices where id = @Id", context);
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (var read = cmd.ExecuteReader())
                    {

                        read.Read();

                        invoiceViewModel.Id = Convert.ToInt32(read["Id"]);
                        invoiceViewModel.Iva = Convert.ToDecimal(read["Iva"]);
                        invoiceViewModel.SubTotal = Convert.ToDecimal(read["SubTotal"]);
                        invoiceViewModel.Total = Convert.ToDecimal(read["Total"]);
                        invoiceViewModel.ClientId = Convert.ToInt32(read["ClientId"]);
                    }



                    //Client:
                    SetClient(invoiceViewModel, context);
                    SetInvoiceDetails(invoiceViewModel, context);
                }

                return invoiceViewModel;
            }
            catch (Exception ex)
            {


                return null;
            }
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
