using KodetiSellsModels;
using RepositoryInterface.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryInterface
{
    public interface IInvoiceRepository : IReadRepository<Invoice, int>, ICreateRespository<Invoice>, IUpdateRepository<Invoice>, IRemoveRepository<int>
    {
       
    }
}
