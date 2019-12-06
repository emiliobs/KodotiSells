using KodotiSellsService;
using System;

namespace KodotiSellsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestService.TestConnection();

            var orderService = new InvoiceService();
            //var result =  orderService.GetAll();
            var result = orderService.GetInvoiceById(55);

            Console.ReadKey();

        }
    }
}
