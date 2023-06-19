using Library.Application;
using Library.Application.Services;
using Library.Domain.Entities;
using Library.Domain.UnitOfWork;
using Library.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        public BookService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void CreateBook(Book book)
        {
            if(_unitOfWork.Books.IsDupplicateBook(book.Name, book.Author))
            {
                throw new DupplicateBookException("The book Book '"+book.Name+"' already exists!");
            }
            _unitOfWork.Books.Add(book);
            _unitOfWork.Save();
        }

        public void DeleteBook(Guid Id)
        {
            _unitOfWork.Books.Remove(Id);
            _unitOfWork.Save();
        }

        public IList<Book> GetAll()
        {
          return  _unitOfWork.Books.GetAll();
        }

        public Book GetBook(Guid Id)
        {
            return _unitOfWork.Books.GetById(Id);
        }

        public async Task<(IList<Book> records, int total, int totalDispaly)> GetPagedBookAsync(
            string searchText, string orderBy, int pageIndex, int pageSize)
        {
            return await _unitOfWork.Books.GetTableDataAsync(
                x => x.Name.Contains(searchText) || x.Author.Contains(searchText) || x.Genre.Contains(searchText),
                orderBy, pageIndex, pageSize);
        }

        public void UpdateBook(Book book, Guid id)
        {
            var model = _unitOfWork.Books.GetById(id);
            if (model != null)
            {
                model.Author = book.Author;
                model.Genre = book.Genre;
                model.Name= book.Name;
            }
            _unitOfWork.Save();
        }
    }
}
