using Microsoft.AspNetCore.Mvc;

namespace TwitterBot.Web.Areas.NewsReviewer.Controllers
{
    [Area(nameof(NewsReviewer))]
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
