using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryInterface.Actions
{
    public interface IReadRepository<T, Y> where T: class
    {
        IEnumerator<T> GetAll();

        T GetById(Y id);
    }
}
