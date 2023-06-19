using Autofac;
using AutoMapper;
using Library.Application.Services;
using Library.Domain.Entities;
using Library.Infrastructure.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Library.Web.Areas.Admin.Models
{
    public class BookCreateModel
    {
        [Required, MinLength(1, ErrorMessage ="Book Name cannot be empty")]
        public string Name { get; set; }

        [Required, MinLength(1, ErrorMessage = "Author Name cannot be empty")]
        public string Author { get; set; }

        [Required, MinLength(1, ErrorMessage = "Genre cannot be empty")]
        public string Genre { get; set; }

        protected IBookService bookService { get; set; }
        protected IMapper _mapper { get; set; }
        public BookCreateModel()
        {

        }

        public BookCreateModel(IBookService bookService, IMapper mapper) 
        {
            this.bookService = bookService;
            this._mapper = mapper;
        }
        public void ResolveDependency(ILifetimeScope scope)
        {
            bookService = scope.Resolve<IBookService>();
            _mapper = scope.Resolve<IMapper>();
        }
        public void CreateBook()
        {
            if(string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Author) || string.IsNullOrWhiteSpace(Genre)) {
                throw new EmptyException("Name or Author or Genre cannot be empty");
            }

            Book book = new Book();
            _mapper.Map(this, book);
       

            bookService.CreateBook(book);
        }
    }
}
