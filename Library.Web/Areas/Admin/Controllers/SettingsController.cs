using Autofac;
using Library.Infrastructure.Utilities;
using Library.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace Library.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles ="Admin1")]
    public class SettingsController : Controller
    {
        private readonly ILifetimeScope _scope;
        public SettingsController(ILifetimeScope scope) 
        {
            _scope = scope;
        }
        public IActionResult Roles()
        {

            return View();
        }

        [HttpGet]
        public IActionResult CreateRole()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(RoleCreateModel role)
        {
            if (role != null)
            {
                role.ResolveDependency(_scope);
                await role.CreateRole();
            }
            return RedirectToAction(nameof(Roles));
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(Guid id)
        {
            var role = _scope.Resolve<RoleEditModel>();
            await role.LoadData(id);
            return View(role);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(RoleEditModel role)
        {
            if (ModelState.IsValid)
            {
                role.ResolveDependency(_scope);
                await role.UpdateRole();
            }
            return RedirectToAction(nameof(Roles));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var role = _scope.Resolve<RoleEditModel>();
            await role.DeleteRole(id);
            return RedirectToAction(nameof(Roles));
        }
        [HttpGet]
        public JsonResult GetRoles()
        {
            var model = _scope.Resolve<RoleListModel>();
            var data = model.GetRoles();
            var x = data;
            return Json(data);

        }
        public async Task<IActionResult> AssignRole()
        {
            var model = _scope.Resolve<RoleAssignModel>();
            await model.LoadData();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(RoleAssignModel model)
        {
            model.ResolveDependency(_scope);
            await model.AssignRole();
            return RedirectToAction(nameof(Roles));
        }

        public async Task<IActionResult> AssignClaim()
        {
            var model = _scope.Resolve<RoleAssignModel>();
            await model.AssignStaticClaim();
            return View(model);
        }
    }
}
