using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterBot.Core.IRepository;
using TwitterBot.Core.Models;
using TwitterBot.DataAccess.Data;

namespace TwitterBot.DataAccess.Repository
{
    public class NewsRepository : Repository<News>, INewsRepository
    {
        public NewsRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
