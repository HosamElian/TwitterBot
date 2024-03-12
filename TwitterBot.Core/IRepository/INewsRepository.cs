using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterBot.Core.Interfaces;
using TwitterBot.Core.Models;

namespace TwitterBot.Core.IRepository
{
    public interface INewsRepository : IRepository<News>
    {
    }
}
