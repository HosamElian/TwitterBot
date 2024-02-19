using Microsoft.AspNetCore.Mvc;

namespace TwitterBot.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
