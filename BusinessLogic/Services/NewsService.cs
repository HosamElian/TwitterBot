using Microsoft.Extensions.Configuration;
using NewsAPI;
using NewsAPI.Models;
using System.Linq.Expressions;
using TwitterBot.Core;
using TwitterBot.Core.IRepository;
using TwitterBot.Core.IServices;
using TwitterBot.Core.Models;
using TwitterBot.Web.Areas.NewsReviewer.ViewModel;

namespace BusinessLogic.Services
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;
        IConfiguration _configuration;

        public NewsService(IUnitOfWork unitOfWork,
                        IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public void GetAllNewsFromApi(EverythingRequest newsRequest)
        {
            var key = _configuration.GetSection(nameof(Shared.Keys_Holder)).GetSection(nameof(Shared.News_Key)).Value;
         
            var newsApiClient = new NewsApiClient(key);
            var articlesResponse = newsApiClient.GetEverything(newsRequest);

            if (articlesResponse.TotalResults == 0) return;
            var articles = articlesResponse.Articles.Select(a => new News
            {
                Author = a.Author,
                Description = a.Description,
                IsApproved = false,
                OriganalNews = a.Content,
                PublishedAt = a.PublishedAt,
                Url = a.Url,
                UrlToImage = a.UrlToImage,
                Title = a.Title,
                CreatedTime = DateTime.Now,
            });
            _unitOfWork.News.AddRange(articles);
            var saved = _unitOfWork.SaveChanges();

        }

        public IEnumerable<NewsVM>? GetAllNewsFromDb(Expression<Func<News, bool>> filter)
        {
            return _unitOfWork.News.GetAll(filter).Select(a => new NewsVM
            {
                Id = a.Id,
                Author = a.Author,
                Description = a.Description,
                IsApproved = false,
                OriganalNews = a.OriganalNews,
                PublishedAt = a.PublishedAt,
                Url = a.Url,
                UrlToImage = a.UrlToImage,
                Title = a.Title,
                CreatedTime = DateTime.Now,
            });
        }

    }
}
