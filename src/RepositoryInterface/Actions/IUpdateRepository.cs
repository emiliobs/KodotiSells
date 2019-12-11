using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryInterface.Actions
{
    public interface IUpdateRepository<T> where T: class
    {
        void Update(T t);
    }
}
