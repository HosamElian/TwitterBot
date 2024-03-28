using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterBot.Core.IRepository
{
    public interface IUnitOfWork
    {
        public INewsRepository NewsRepository {  get; }

        public bool SaveChanges();
    }
}
