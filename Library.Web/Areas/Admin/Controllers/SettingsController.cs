using Autofac;
using Library.Infrastructure.Utilities;
using Library.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace Library.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles ="Admin")]
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
            return View();
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
    }
}
