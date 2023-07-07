using Library.Application.Services;
using Library.Infrastructure.Utilities;
using Library.Persistence.Features.Memberships;
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Library.Web.Areas.Admin.Models
{
    public class RoleListModel
    {
        private RoleManager<ApplicationRole> _roleManager;
        public RoleListModel() { }

        public RoleListModel(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        internal object GetRoles()
        {
            return new
            {
                data = _roleManager.Roles.Select(x => new { x.Name, Id = x.Id.ToString() }).ToList()
            };
        }

    }
}
