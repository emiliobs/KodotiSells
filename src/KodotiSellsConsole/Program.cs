using KodotiSellsService;
using System;

namespace KodotiSellsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestService.TestConnection();

            var orderService = new OrderService();
            var result =  orderService.GetAll();

            Console.ReadKey();

        }
    }
}
