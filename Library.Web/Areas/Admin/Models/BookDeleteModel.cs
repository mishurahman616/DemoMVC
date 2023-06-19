using Library.Application.Services;

namespace Library.Web.Areas.Admin.Models
{
    public class BookDeleteModel
    {
        private IBookService _bookService;
        public BookDeleteModel()
        {

        }
        public BookDeleteModel(IBookService bookService)
        {
            _bookService = bookService;
        }
        public void DeleteBook(Guid Id)
        {
            _bookService.DeleteBook(Id);
        }
    }
}
