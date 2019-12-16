using KodetiSellsModels;
using RepositoryInterface.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryInterface
{
    public interface IInvoiceDetailsRepository : IReadRepository<InvoiceDetail,int>
    {
        void Create(IEnumerable<InvoiceDetail> invoiceDetails, int invoiceId);
        IEnumerable<InvoiceDetail> GetAllInvoicedetailsById(int invocieId);
        void Update(InvoiceViewModel  invoiceViewModel);
        void RemoveInvoiceIdById(int id);
    }
}
