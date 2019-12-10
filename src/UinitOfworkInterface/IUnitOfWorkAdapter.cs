using System;
using System.Collections.Generic;
using System.Text;

namespace UinitOfworkInterface
{
    public interface IUnitOfWorkAdapter : IDisposable
    {
        IUnitOfWorkRepository Repository { get; }
        void SaveChange();
    }
}
