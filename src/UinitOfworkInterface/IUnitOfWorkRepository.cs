using RepositoryInterface;

namespace UinitOfworkInterface
{
    public interface IUnitOfWorkRepository
    {
        IInvoiceRepository InvoiceRepository { get; }
        IProductRepository ProductRepository{ get; }
        IInvoiceDetailsRepository InvoiceDetailsRepository { get; }
        IClientRepository ClientRepository { get; }

    }
}
