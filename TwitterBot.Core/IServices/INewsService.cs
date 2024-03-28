using System;
using NewsAPI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterBot.Core.Models;
using NewsAPI.Models;
using System.Linq.Expressions;
using TwitterBot.Web.Areas.NewsReviewer.ViewModel;
namespace TwitterBot.Core.IServices
{
    public interface INewsService
    {
        public void GetAllNewsFromApi(EverythingRequest newsRequest);
        public IEnumerable<NewsVM>? GetAllNewsFromDb(Expression<Func<News, bool>> filter);
    }
}
