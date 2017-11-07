using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Wispero.Entities;

namespace Wispero.Core.Services
{
    public interface IQueryService<T> where T: class
    {
        T Get(int id);
        List<T> GetAll();
        List<T> GetByFilter(Expression<Func<T, bool>> expression);
    }
}
