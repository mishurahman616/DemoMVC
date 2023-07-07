using Library.Domain.Entities;
using Library.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Repositories
{
    public interface IBookRepository : IRepositoryBase<Book, Guid>
    {
        public bool IsDupplicateBook(string bookName, string authorName, Guid? id = null);
        public Task<(IList<Book> records, int total, int totalDisplay)> GetTableDataAsync(
            Expression<Func<Book, bool>> filter = null,
            string orderBy = null,
            int pageIndex = 1, int pageSize = 10);
    }
}
