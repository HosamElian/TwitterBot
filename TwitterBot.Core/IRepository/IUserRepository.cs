using TwitterBot.Core.Interfaces;
using TwitterBot.Core.Models;
using TwitterBot.Core.NoDbModels;
using TwitterBot.Core.ViewModel;

namespace TwitterBot.Core.IRepository
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        IEnumerable<UserVM> GetUserVMs(Paggination paggination = null);
        void Update(UserVM userVM);
        public string GetRoleName(string userId);
    }
}
