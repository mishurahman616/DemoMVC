using Autofac;
using AutoMapper;
using Library.Application.Services;
using Library.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library.API.Models
{
    public class BookModel
    {
        private  IBookService _bookService;
        private  IMapper? _mapper;
       
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public BookModel()
        {

        }
        public BookModel(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _bookService = scope.Resolve<IBookService>();
            _mapper = scope.Resolve<IMapper>();
        }
        internal Book GetBookById(Guid id)
        {
            return _bookService.GetBook(id);
        }

        internal IEnumerable<Book> GetAllBooks()
        {

            return _bookService.GetAll().ToList();
       
        }
        internal void CreateBook()
        {
            var book = new Book();
            _mapper.Map(this, book);

            _bookService.CreateBook(book);
        }

        internal void UpdateBook()
        {
            var book = new Book();
            _mapper.Map(this, book);
            _bookService.UpdateBook(book, book.Id);          
        }
        internal void DeleteBook(Guid id)
        {
            _bookService.DeleteBook(id);
        }
    }
}
