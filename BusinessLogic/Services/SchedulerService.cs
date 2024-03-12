using NewsAPI.Constants;
using NewsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterBot.Core.IRepository;
using TwitterBot.Core.IServices;
using TwitterBot.Core.Models;
using TwitterBot.Core.NoDbModels;

namespace BusinessLogic.Services
{
    public class SchedulerService : ISchedulerService
    {
        private readonly INewsService _newsService;
        private readonly IChatGPTService _chatGPTService;
        private readonly ITwitterHandlerService _twitterHandlerService;
        private readonly IUnitOfWork _unitOfWork;

        public SchedulerService(INewsService newsService,
            IChatGPTService chatGPTService,
            ITwitterHandlerService twitterHandlerService,
            IUnitOfWork unitOfWork)
        {
            _newsService = newsService;
            _chatGPTService = chatGPTService;
            _twitterHandlerService = twitterHandlerService;
            _unitOfWork = unitOfWork;
        }
        public void Run()
        {
            var newsFromAPI = _newsService.GetAllNews(new EverythingRequest
            {
                SortBy = SortBys.Popularity,
                Language = Languages.AR,
                From = DateTime.Now.AddHours(-30),
            });

            if (newsFromAPI == null) return;
            //Make Decision
            //save data
            var news = _unitOfWork.NewsRepository.GetLastOrDefualt(c => c.IsApproved && c.DecisionTime > DateTime.UtcNow.AddHours(5));
            var formattednews = _chatGPTService.SendChatMessage(news.OriganalNews).Result.Select(x => x.Content).ToList();
            var IsOntwitter = _twitterHandlerService.PostTweet(new PostTweetRequestDto { Text = formattednews.FirstOrDefault()  ?? ""});
        }
    }
}
