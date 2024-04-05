using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TwitterBot.Core;
using TwitterBot.Core.IRepository;
using TwitterBot.Core.IServices;
using TwitterBot.Core.NoDbModels;

namespace TwitterBot.Web.Areas.NewsReviewer.Controllers
{
    [Area(nameof(NewsReviewer))]
    [Authorize(Roles = Shared.Role_NewsReviewer)]
    public class NewsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewsService _newsService;
        private readonly IChatGPTService _chatGPT;
        private readonly ITwitterHandlerService _twitterHandler;

        public NewsController(IUnitOfWork unitOfWork,
            INewsService newsService,
            IChatGPTService chatGPT,
            ITwitterHandlerService twitterHandler)
        {
            _unitOfWork = unitOfWork;
            _newsService = newsService;
            _chatGPT = chatGPT;
            _twitterHandler = twitterHandler;
        }
        public IActionResult Index()
        {
            var news = _newsService.GetAllNewsFromDb(c => !c.IsApproved || !c.IsExpired);
            return View(news);
        }
        [HttpPost]
        public IActionResult Paraphrasing(int id)
        {
            var news = _unitOfWork.News.GetFirstOrDefualt(n => n.Id == id);
            if (news != null)
            {
                var done = _chatGPT.SendChatMessage(news).Result;
            }
            _unitOfWork.SaveChanges();
            return Redirect(nameof(Index));
        }
        [HttpPost]
        public IActionResult PostOnTwitter(int id)
        {
            var news = _unitOfWork.News.GetFirstOrDefualt(n => n.Id == id);
            if (news != null)
            {
                var done = _twitterHandler.PostTweet(new PostTweetRequestDto
                {
                    Text = news.ParaphrasdNews ?? news.OriganalNews,
                }).Result;
                if (done)
                {
                    news.IsApproved = true;
                    news.DecisionById = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    news.DecisionTime = DateTime.Now;
                }
                _unitOfWork.SaveChanges();
            }
            return Redirect(nameof(Index));
        }
        [HttpPost]
        public IActionResult Rejected(int id)
        {
            var news = _unitOfWork.News.GetFirstOrDefualt(n => n.Id == id);
            if (news != null)
            {
                news.IsExpired = true;
                _unitOfWork.SaveChanges();
            }
            return Redirect(nameof(Index));
        }
    }
}
