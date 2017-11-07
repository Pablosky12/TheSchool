using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wispero.Core.Services
{
    public interface IDataService<T>
    {
        void Add(T entity);
        void Edit(T entity);
        void Delete(int id);

        void CommitChanges();
    }
}
