using KodetiSellsModels;
using RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositorySQLServer
{
    public class ClientRepository : Repository, IClientRepository
    {
        public IEnumerator<Client> GetAll()
        {
            throw new NotImplementedException();
        }

        public Client GetById(int id)
        {
            var cmd = CreateCommnad("Select * From Clients With(NOLOCK) Where id = @ClientId");
            cmd.Parameters.AddWithValue("@ClientId", id);

            using (var reader = cmd.ExecuteReader())
            {
                reader.Read();

                return  new Client
                {
                    Id = Convert.ToInt32("@clientId", id),
                    Name = reader["Name"].ToString(),
                };

            }
        }
    }
}
