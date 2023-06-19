using Autofac;
using Library.Application.Services;
using Library.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Library.Web.Areas.Admin.Models
{
    public class BookEditModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required, MinLength(1, ErrorMessage = "Book Name cannot be empty")]
        public string Name { get; set; }

        [Required, MinLength(1, ErrorMessage = "Author Name cannot be empty")]
        public string Author { get; set; }

        [Required, MinLength(1, ErrorMessage = "Genre cannot be empty")]
        public string Genre { get; set; }

        protected IBookService bookService { get; set; }
        public BookEditModel() { }
        public BookEditModel(IBookService bookService)
        {
            this.bookService = bookService;
        }
        public void ResolveDependency(ILifetimeScope scope)
        {
            bookService = scope.Resolve<IBookService>();
        }
        public void Load(Guid id)
        {
            Book book = bookService.GetBook(id);
            if (book is null)
            {
                throw new Exception("Book Not Found");
            }
                Name= book.Name;
                Author= book.Author;
                Genre= book.Genre;
            
        }
        public void UpdateBook()
        {
            Book book = new Book();
            book.Name = Name;
            book.Author = Author;
            book.Genre = Genre;
            bookService.UpdateBook(book, Id);
        }
    }
}
