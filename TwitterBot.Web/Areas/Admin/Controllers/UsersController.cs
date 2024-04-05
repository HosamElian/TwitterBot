using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TwitterBot.Core;
using TwitterBot.Core.IRepository;
using TwitterBot.Core.Models;
using TwitterBot.Core.NoDbModels;
using TwitterBot.Core.ViewModel;
using TwitterBot.DataAccess.Repository;

namespace TwitterBot.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = Shared.Role_Admin)]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(IUnitOfWork unitOfWork, 
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public ActionResult<IEnumerable<UserVM>> Index()
        {
            var users = _unitOfWork.User.GetUserVMs();
            return View(users);
        }
        public async Task<ActionResult<UserVM>> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var userVM = new UserVM 
            { 
                Id = user.Id, 
                Name = user.UserName, 
                IsActive = user.IsActive,
                Role = _unitOfWork.User.GetRoleName(id),
                RoleList =  _roleManager.Roles.Select(r => r.Name).Select(r => new SelectListItem()
                {
                    Text = r,
                    Value = r
                }),
            };
            return View(userVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<UserVM> Edit(UserVM userVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.User.Update(userVM);
                _unitOfWork.SaveChanges();

                TempData["success"] = "User Updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(userVM);
        }
        [HttpDelete]
        public IActionResult Delete(string? id)
        {
            var userfromDb = _unitOfWork.User.GetFirstOrDefualt(u => u.Id == id);
            if (userfromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }


            _unitOfWork.User.Remove(userfromDb);
            _unitOfWork.SaveChanges();
            return Json(new { success = true, message = "User deleted successfully" });
        }
    }
}
