using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryInterface.Actions
{
    public interface IRemoveRepository<T>
    {
        void Remove(T id);
    }
}
