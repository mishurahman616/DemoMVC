using Autofac;
using Library.Persistence.Features.Memberships;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace Library.Web.Areas.Admin.Models
{
    public class RoleEditModel
    {
        [Required(ErrorMessage = "Invalid Role Id")]
        public Guid RoleId { get; set; }
        [Required, MinLength(2, ErrorMessage = "Name shoud contain at leat two characters")]
        [MaxLength(20, ErrorMessage = "Name shoud contain at leat two characters")]
        public string RoleName { get; set; }

        private RoleManager<ApplicationRole>? _roleManager;
        public RoleEditModel()
        {

        }
        public RoleEditModel(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }
        internal async Task LoadData(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                throw new InvalidOperationException("Role not found");
            }
            RoleId = id;
            RoleName = role.Name;
        }
        internal void ResolveDependency(ILifetimeScope scope)
        {
            _roleManager = scope.Resolve<RoleManager<ApplicationRole>>();
        }
        public async Task UpdateRole()
        {
            RoleName = RoleName.Trim();
            if (string.IsNullOrWhiteSpace(RoleName) || Guid.Empty == RoleId || _roleManager==null) 
            {
                throw new Exception("Invalid Role");
            }

            var role = await _roleManager.FindByIdAsync(RoleId.ToString());
            if(role!= null)
            { 
                role.Name = RoleName;
              await _roleManager.UpdateAsync(role);
            }

        }
        public async Task DeleteRole(Guid id)
        {
            if (Guid.Empty == id || _roleManager == null)
            {
                throw new Exception("Invalid Role");
            }
            ApplicationRole role = await _roleManager.FindByIdAsync(id.ToString());

            await _roleManager.DeleteAsync(role);
        }
    }
}
