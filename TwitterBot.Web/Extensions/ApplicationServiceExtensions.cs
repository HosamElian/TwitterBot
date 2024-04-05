using BusinessLogic.Services;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TwitterBot.Core.IRepository;
using TwitterBot.Core.IServices;
using TwitterBot.Core.Models;
using TwitterBot.DataAccess.Data;
using TwitterBot.DataAccess.Repository;

namespace TwitterBot.Web.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services,
        IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddHangfire(option => 
            option.UseSqlServerStorage(connectionString));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<ISchedulerService, SchedulerService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IChatGPTService, ChatGPTService>();
            services.AddScoped<ITwitterHandlerService, TwitterHandlerService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<IUserService, UserService>();

            services.AddCors();
            return services;
        }
    }
}
