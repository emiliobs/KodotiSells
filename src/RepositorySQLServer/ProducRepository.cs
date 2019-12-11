using KodetiSellsModels;
using RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositorySQLServer
{
    public class ProducRepository : Repository, IProductRepository
    {
        public IEnumerator<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public Product GetById(int id)
        {
            var cmd = CreateCommnad("Select * From Products With(NOLOCK) Where Id = @Id");
            cmd.Parameters.AddWithValue("@ProductId",id);

            using (var reader = cmd.ExecuteReader())
            {
                return new Product
                {
                    Id = Convert.ToInt32(reader["@Id"]),
                    Price = Convert.ToDecimal(reader["Price"]),
                    Name = reader["Name"].ToString()
                };
            }

        }
    }
}
