﻿using KodetiSellsModels;
using KodotiSellsService;
using System;
using System.Collections.Generic;
using UnitOfWorkSqlServer;

namespace KodotiSellsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestService.TestConnection();

            //var orderService = new InvoiceService();

            //var result = orderService.GetAll();
            //var result = orderService.GetInvoiceById(55);

            var unitOfWork = new UnityOfWorkSqlServer();

            var invoceService = new InvoiceService(unitOfWork);

            var invoice = new InvoiceViewModel
            {
                Id = 2,
                ClientId = 2,
                InvoiceDetails = new List<InvoiceDetail>
                 {
                     new InvoiceDetail
                     {
                       ProductId = 3,
                       Quantity = 55,
                       Price = 5000,
                     },
                     new InvoiceDetail
                     {
                        ProductId = 5,
                        Quantity = 55,
                        Price = 5000,
                     },
                 },
            };

            invoceService.Create(invoice);

            //var invoice = new InvoiceViewModel
            //{
            //    ClientId = 1,
            //    InvoiceDetails = new List<InvoiceDetail>
            //     {
            //         new InvoiceDetail
            //         {
            //           ProductId = 3,
            //           Quantity = 5,
            //           Price = 12,
            //         },
            //         new InvoiceDetail
            //         {
            //            ProductId = 5,
            //            Quantity = 10,
            //            Price = 32,
            //         },
            //     },
            //};



            //orderService.Create(invoice);

            //var invoice = new InvoiceViewModel
            //{
            //    Id = 5,
            //    ClientId = 1,
            //    InvoiceDetails = new List<InvoiceDetail>
            //     {
            //         new InvoiceDetail
            //         {
            //           ProductId = 3,
            //           Quantity = 5,
            //           Price = 1254,
            //         },
            //         new InvoiceDetail
            //         {
            //            ProductId = 5,
            //            Quantity = 30,
            //            Price = 125,
            //         },
            //     },
            //};

            //orderService.Create(invoice);

            //InvoiceService invoices = new InvoiceService();

            //invoices.Delete(5);


            Console.ReadKey();

        }
    }
}
