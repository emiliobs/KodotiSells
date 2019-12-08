using KodetiSellsModels;
using KodotiSellsService;
using System;
using System.Collections.Generic;

namespace KodotiSellsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestService.TestConnection();

            var orderService = new InvoiceService();
            //var result =  orderService.GetAll();
            //var result = orderService.GetInvoiceById(55);

            //Prepar factura:
            var invoice = new InvoiceViewModel 
            {
                 ClientId = 1,
                 InvoiceDetails = new List<InvoiceDetail> 
                 {
                     new InvoiceDetail
                     {
                       ProductId = 3,
                       Quantity = 5,
                       Price = 1254,
                     },
                     new InvoiceDetail
                     {
                        ProductId = 5,
                        Quantity = 10,
                        Price = 3214,
                     },
                 },
            };

            orderService.Create(invoice);


            Console.ReadKey();

        }
    }
}
