using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Interfaces;
using ToDo.Application.Services;
using ToDo.Domain.Interfaces;
using ToDo.Domain.Models;
using ToDo.Infrastructure.Context;
using ToDo.Infrastructure.Repositories;
using ToDo.Infrastructure.UnitOfWork;

namespace ToDo.Application.Dependency
{
    public static class DependencyContainer
    {
        public static IServiceCollection RegisterToDo(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ToDoDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")).UseLazyLoadingProxies());

            services.AddAuthentication().AddFacebook(options => {
                options.AppId = configuration["Authentication:Facebook:AppId"];
                options.AppSecret = configuration["Authentication:Facebook:AppSecret"];
            });

            services.AddDefaultIdentity<User>(options => {
                options.SignIn.RequireConfirmedAccount = true;

                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;

                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcçdefgğhıijklmnoöpqrsştuüvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";
            })
                .AddEntityFrameworkStores<ToDoDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options => {
                options.LoginPath = "/Auth/Login";
                options.LogoutPath = "/Auth/Logout";
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IMissionRepository, MissionRepository>();
            services.AddScoped<IMissionService, MissionService>();
            services.AddScoped<IUserService, UserService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
