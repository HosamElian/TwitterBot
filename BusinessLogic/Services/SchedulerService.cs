using NewsAPI.Constants;
using NewsAPI.Models;
using TwitterBot.Core.IRepository;
using TwitterBot.Core.IServices;

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
            return;
            _newsService.GetAllNewsFromApi(new EverythingRequest
            {
                Q = "kuwait",
                SortBy = SortBys.Popularity,
                Language = Languages.EN,
            });


            var newsFromDB = _unitOfWork.NewsRepository.GetAll(c => !c.IsApproved && !c.IsExpired);
            if (newsFromDB == null) return;
            var formattednews = _chatGPTService.SendChatMessage(newsFromDB).Result;
        }
    }
}
