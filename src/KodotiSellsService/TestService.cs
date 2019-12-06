using KodotiSellsCommon;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace KodotiSellsService
{
    public class TestService
    {
        public static void TestConnection()
        {
			try
			{
				using (var context = new SqlConnection(Parameters.Connectionstring))
				{
					context.Open();
					Console.WriteLine("SQl Connection Successful.!!!");
				}
			}
			catch (Exception ex)
			{

				Console.WriteLine($"Sql Server: {ex.Message}");
			}
        }
    }
}
