using Library.Infrastructure.Securities.Authorization;
using Library.Persistence.Features.Memberships;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Persistence.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserManager<ApplicationUserManager>()
                .AddRoleManager<ApplicationRoleManager>()
                .AddSignInManager<ApplicationSignInManager>()
                .AddDefaultTokenProviders();

            services.AddAuthentication()
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Account/Login");
                    options.LogoutPath = new PathString("/Account/Logout");
                    options.Cookie.Name = "LibraryPortal.Identity";
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromHours(1);
                    
                });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("HRLibraianPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("HR");
                    policy.RequireRole("Librarian");
                });
                options.AddPolicy("ViewBookPolicy", policy => {
                    policy.RequireClaim("ViewBookClaim", "True");
                });
                options.AddPolicy("BookCreateRequirementPolicy", policy =>
                {
                    policy.Requirements.Add(new BookCreateRequirement());
                });
            });
            services.AddSingleton<IAuthorizationHandler, BookCreateRequirementHandler>();
            services.AddRazorPages();
        }
        public static void AddGoogleAuth(this IServiceCollection services)
        {
            services.AddGoogleAuth();
        }
    }
}
