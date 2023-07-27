using Autofac;
using Library.API.Models;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class BookController : Controller
    {
        private ILifetimeScope _scope;
        private readonly ILogger<BookController> _logger;

        public BookController(ILifetimeScope scope, ILogger<BookController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        [HttpGet("id")]
        //[Authorize(Policy = "BookCreateRequirementPolicy")]
        public Book GetBook(Guid id)
        {
            BookModel model = _scope.Resolve<BookModel>();
            return model.GetBookById(id);

        }

        [HttpGet]
        public IEnumerable<Book> GetBooks()
        {
            BookModel model = _scope.Resolve<BookModel>();
            return model.GetAllBooks();
        }

        [HttpPost]
        public IActionResult CreateBook([FromBody] BookModel bookModel)
        {
            try
            {
                bookModel.ResolveDependency(_scope);
                bookModel.CreateBook();
                return Ok("Book Created Successfully!");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
       
        }

        [HttpPut]
        public IActionResult UpdateBook([FromBody] BookModel bookModel)
        {
            try
            {
                bookModel.ResolveDependency(_scope);
                bookModel.UpdateBook();
                return Ok("Book Updated Successfully");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete]
        public IActionResult DeleteBook(Guid id)
        {
            try
            {
                BookModel model = _scope.Resolve<BookModel>();
                model.DeleteBook(id);
                return Ok($"Book With Id: {id} deleted Successfully");
            }catch (Exception ex){
                return BadRequest(ex.Message);
            }
        }
    }
}
