using Microsoft.AspNetCore.Identity;
using TwitterBot.Core.IRepository;
using TwitterBot.Core.Models;
using TwitterBot.Core.NoDbModels;
using TwitterBot.Core.ViewModel;
using TwitterBot.DataAccess.Data;

namespace TwitterBot.DataAccess.Repository
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
        }
        public IEnumerable<UserVM> GetUserVMs(Paggination paggination = null)
        {
            return _context.Users
                //.Skip((paggination.Current - 1) * paggination.PageSize)
                //.Take(paggination.PageSize)
                .Select(u => new UserVM
                {
                    Id = u.Id,
                    Name = u.UserName ?? "Name",
                    Role = _context.Roles.FirstOrDefault(r => r.Id ==
                    (_context.UserRoles.FirstOrDefault(ur => ur.UserId == u.Id).RoleId
                    )).Name ?? "No Role Found"
                });
        }

        public void Update(UserVM userVM)
        {
            var user = _context.Users.Find(userVM.Id);
            if (user != null)
            {
                user.IsActive = userVM.IsActive;
            }

            var userCurrentRole = _userManager.GetRolesAsync(user).Result.FirstOrDefault();


            if (userVM.Role != null)
            {
                var role = _context.Roles.FirstOrDefault(r => r.Name == userVM.Role);
                if (role != null)
                {
                    if (!_userManager.IsInRoleAsync(user, role.Name).GetAwaiter().GetResult())
                    {
                        _userManager.RemoveFromRoleAsync(user, userCurrentRole);
                        _userManager.AddToRoleAsync(user, role.Name).GetAwaiter().GetResult();
                    }
                }

            }
        }

        public string GetRoleName(string userId)
        {
            var roleId = _context.UserRoles.FirstOrDefault(ur => ur.UserId == userId)?.RoleId;
            if (roleId != null)
            {
                return _context.Roles.FirstOrDefault(r => r.Id == roleId)?.Name;
            }
            return "No Role Found";
        }
    }
}
