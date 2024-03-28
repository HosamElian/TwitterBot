using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TwitterBot.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T GetFirstOrDefualt(Expression<Func<T, bool>> filter, string? indcludeProperties = null, bool tracked = true);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? indcludeProperties = null);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        T GetLastOrDefualt(Expression<Func<T, bool>> filter, string? indcludeProperties = null, bool tracked = true);
        void RemoveRange(IEnumerable<T> entities);

    }
}
