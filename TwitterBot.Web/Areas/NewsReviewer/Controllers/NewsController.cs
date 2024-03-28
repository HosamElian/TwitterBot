using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterBot.Core;
using TwitterBot.Core.IServices;

namespace TwitterBot.Web.Areas.NewsReviewer.Controllers
{
    [Area(nameof(NewsReviewer))]
    [Authorize(Roles = Shared.Role_NewsReviewer)]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
        public IActionResult Index()
        {
            var news = _newsService.GetAllNewsFromDb(c => !c.IsApproved);
            return View(news);
        }
    }
}
