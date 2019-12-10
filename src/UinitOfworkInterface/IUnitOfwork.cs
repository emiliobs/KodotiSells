using System;
using System.Collections.Generic;
using System.Text;

namespace UinitOfworkInterface
{
    public interface IUnitOfwork
    {
        IUnitOfWorkAdapter Create();
    }
}
