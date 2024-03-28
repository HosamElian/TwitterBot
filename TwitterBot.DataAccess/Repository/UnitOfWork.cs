using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterBot.Core.IRepository;
using TwitterBot.DataAccess.Data;

namespace TwitterBot.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            NewsRepository = new NewsRepository(_context);
        }
        public INewsRepository NewsRepository { get; private set; }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
