using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterBot.Core.IRepository;
using TwitterBot.Core.Models;
using TwitterBot.DataAccess.Data;

namespace TwitterBot.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UnitOfWork(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            News = new NewsRepository(_context);
            User = new UserRepository(_context, _userManager);
        }
        public INewsRepository News { get; private set; }
        public IUserRepository User { get; private set; }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
