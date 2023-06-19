using Autofac;
using Library.Infrastructure.Utilities;
using Library.Web.Areas.Admin.Models;
using Library.Web.Models;
using Library.Web.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<BookController> _logger;
        public BookController(ILifetimeScope scope, ILogger<BookController> logger)
        {
            _scope = scope;
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }
        //Http Get
        public IActionResult Create()
        {
            return View();
        }
        //Http Post
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(BookCreateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.ResolveDependency(_scope);
                    model.CreateBook();
                    TempData.Put<ResponseModel>("Response", new ResponseModel
                    {
                        Message = "Book Successfully Created! ",
                        ResponseType = ResponseType.Success,
                    });
                    return RedirectToAction("Index");
            }catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                TempData.Put<ResponseModel>("Response", new ResponseModel
                {
                    Message = ex.Message,
                    ResponseType = ResponseType.Danger,
                });
            }
        }
            return View(model);
        }
        public IActionResult Edit(Guid Id)
        {
            BookEditModel? model = null;
            try
            {
                model = _scope.Resolve<BookEditModel>();
                model.Load(Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                TempData.Put<ResponseModel>("Response", new ResponseModel
                {
                    Message = ex.Message,
                    ResponseType = ResponseType.Danger,
                });
            }

            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(BookEditModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.ResolveDependency(_scope);
                    model.UpdateBook();
                    TempData.Put<ResponseModel>("Response", new ResponseModel
                    {
                        Message = "Book info updated Successfully!",
                        ResponseType = ResponseType.Success,
                    });
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                    TempData.Put<ResponseModel>("Response", new ResponseModel
                    {
                        Message = ex.Message,
                        ResponseType = ResponseType.Danger,
                    });
                }

            }
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var id = Request.Form["id"];
                    var guid = new Guid(id!);
                    var model = _scope.Resolve<BookDeleteModel>();
                    model.DeleteBook(guid);
                    TempData.Put<ResponseModel>("Response", new ResponseModel
                    {
                        Message = "Book Deleted Successfully!",
                        ResponseType = ResponseType.Success,
                    });
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.ToString());
                    TempData.Put<ResponseModel>("Response", new ResponseModel
                    {
                        Message = "Book Delete Failed!",
                        ResponseType = ResponseType.Danger,
                    });
                }
            }
            else
            {
                TempData.Put<ResponseModel>("Response", new ResponseModel
                {
                    Message = "Book Delete Failed!",
                    ResponseType = ResponseType.Danger,
                });
            }
            return RedirectToAction("Index");
        }
        public async Task<JsonResult> GetBooks()
        {
            var model = _scope.Resolve<BookListModel>();
            var dataTableUtility = new DataTableHttpRequestUtility(Request);
            var result = await model.GetPagedBookAsync(dataTableUtility);
            return Json(result);
        }
    }
}
