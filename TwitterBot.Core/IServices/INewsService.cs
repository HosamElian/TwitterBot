using System;
using NewsAPI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterBot.Core.Models;
using NewsAPI.Models;
namespace TwitterBot.Core.IServices
{
    public interface INewsService
    {
        public IEnumerable<Article>? GetAllNews(EverythingRequest newsRequest);
    }
}
