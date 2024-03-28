using NewsAPI;
using NewsAPI.Models;
using System.Linq.Expressions;
using TwitterBot.Core.IRepository;
using TwitterBot.Core.IServices;
using TwitterBot.Core.Models;
using TwitterBot.Web.Areas.NewsReviewer.ViewModel;

namespace BusinessLogic.Services
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void GetAllNewsFromApi(EverythingRequest newsRequest)
        {
            var newsApiClient = new NewsApiClient("be15a946731948e58270aba6a6f80829");
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
            _unitOfWork.NewsRepository.AddRange(articles);
            var saved = _unitOfWork.SaveChanges();

        }

        public IEnumerable<NewsVM>? GetAllNewsFromDb(Expression<Func<News, bool>> filter)
        {
            return _unitOfWork.NewsRepository.GetAll(filter).Select(a => new NewsVM
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
