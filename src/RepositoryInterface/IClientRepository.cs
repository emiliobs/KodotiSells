using KodetiSellsModels;
using RepositoryInterface.Actions;

namespace RepositoryInterface
{
    public  interface IClientRepository: IReadRepository<Client, int>
    {

    }
}