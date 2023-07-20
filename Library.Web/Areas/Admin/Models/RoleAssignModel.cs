using Autofac;
using AutoMapper;
using Library.Application.Services;
using Library.Domain.Entities;
using Library.Infrastructure.Exceptions;
using Library.Persistence.Features.Memberships;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Library.Web.Areas.Admin.Models
{
    public class RoleAssignModel
    {
        [Required, MinLength(1, ErrorMessage = "Role Name cannot be empty")]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        [Required, MinLength(1, ErrorMessage = "User Name cannot be empty")]
        [Display(Name ="User Name")]
        public string UserName { get; set; }
        public List<SelectListItem> Users { get; set; }
        public List<SelectListItem> Roles { get; set; }
        
        protected RoleManager<ApplicationRole> _roleManager;
        protected UserManager<ApplicationUser> _userManager;
        public RoleAssignModel()
        {

        }

        public RoleAssignModel(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _roleManager = scope.Resolve<RoleManager<ApplicationRole>>();
            _userManager = scope.Resolve<UserManager<ApplicationUser>>();
        }
        internal async Task LoadData()
        {
            Users = await (from user in _userManager.Users
                    select new SelectListItem($"{user.FirstName} {user.LastName} - {user.UserName}", user.UserName)).ToListAsync();
            Roles = await (from role in _roleManager.Roles
                     select new SelectListItem(role.Name, role.Name)).ToListAsync();
        }
        internal async Task AssignRole()
        {
            var User = await _userManager.FindByNameAsync(UserName);
            await _userManager.AddToRoleAsync(User!, RoleName);

        }
        internal async Task AssignStaticClaim()
        {
            try
            {
                ApplicationUser? user = await _userManager.FindByNameAsync("rahman@gmail.com");
                if (user != null)
                {
                    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("ViewCreateClaim", "True"));
                }
                else
                {
                    Console.WriteLine("User not found");
                }
                
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
