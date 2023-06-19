using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Services
{
    public interface IBookService
    {
        public void CreateBook(Book book);
        public void UpdateBook(Book book, Guid id);
        public void DeleteBook(Guid Id);
        public Book GetBook(Guid Id);
        public IList<Book> GetAll();
        public Task<(IList<Book> records, int total, int totalDispaly)> GetPagedBookAsync(
            string searchText, string orderBy,int pageIndex, int pageSize);

    }
}
