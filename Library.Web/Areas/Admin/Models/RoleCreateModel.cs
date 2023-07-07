using Autofac;
using AutoMapper;
using Library.Application.Services;
using Library.Domain.Entities;
using Library.Infrastructure.Exceptions;
using Library.Persistence.Features.Memberships;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Library.Web.Areas.Admin.Models
{
    public class RoleCreateModel
    {
        [Required, MinLength(1, ErrorMessage = "Role Name cannot be empty")]
        public string Name { get; set; }

        protected RoleManager<ApplicationRole> _roleManager;
        protected UserManager<ApplicationUser> _userManager;
        public RoleCreateModel()
        {

        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _roleManager = scope.Resolve<RoleManager<ApplicationRole>>();
            _userManager = scope.Resolve<UserManager<ApplicationUser>>();
        }
        internal async Task CreateRole()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new EmptyException("Role Name cannot be empty");
            }
            await _roleManager.CreateAsync(new ApplicationRole(Name));

        }
    }
}
