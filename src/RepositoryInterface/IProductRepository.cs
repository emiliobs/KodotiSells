using KodetiSellsModels;
using RepositoryInterface.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryInterface
{
    public interface IProductRepository  : IReadRepository<Product, int>
    {
    }
}
