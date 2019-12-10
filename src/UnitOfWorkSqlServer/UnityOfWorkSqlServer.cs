using System;
using System.Collections.Generic;
using System.Text;
using UinitOfworkInterface;

namespace UnitOfWorkSqlServer
{
    public class UnityOfWorkSqlServer : IUnitOfwork
    {
        public IUnitOfWorkAdapter Create()
        {
            return new UnitOfWorkSqlServerAdapter();
        }
    }
}
