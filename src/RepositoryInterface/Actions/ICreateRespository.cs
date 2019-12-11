using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryInterface.Actions
{
    public interface ICreateRespository<T> where T :class
    {
        void Create(T t);
    }
}
