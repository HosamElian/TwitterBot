using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterBot.Core.IServices;

namespace BusinessLogic.Services
{
    public class NewsService : INewsService
    {
        public IEnumerable<Article>? GetAllNews(EverythingRequest newsRequest)
        {
            var newsApiClient = new NewsApiClient("be15a946731948e58270aba6a6f80829");
            var articlesResponse = newsApiClient.GetEverything(newsRequest);
            
            if(articlesResponse.TotalResults == 0)
                return null;

             return articlesResponse.Articles;
            
        }
    }
}
