using Library.Application.Repositories;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Persistence.Repositories
{
    public class BookRepository : Repository<Book, Guid>, IBookRepository
    {
        public BookRepository(IApplicationDbContext dbContext) : base((DbContext)dbContext)
        {
        }

        public async Task<(IList<Book> records, int total, int totalDisplay)> GetTableDataAsync(
            Expression<Func<Book, bool>> filter = null, 
            string orderBy = null, int pageIndex = 1, int pageSize = 10)
        {
            return await GetDynamicAsync(filter, orderBy, null, pageIndex, pageSize, true);
        }

        public bool IsDupplicateBook(string bookName, string authorName, Guid? id=null)
        {
            int bookCount = 0;
            if (id.HasValue)
            {
                bookCount = GetCount(x => x.Name == bookName && x.Author == authorName && x.Id != id);
            }
            else
            {
                bookCount = GetCount(x => x.Name == bookName && x.Author == authorName);
            }
            return bookCount> 0;
        }
    }
}
