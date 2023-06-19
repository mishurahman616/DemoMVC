using Library.Application.Services;
using Library.Infrastructure.Utilities;

namespace Library.Web.Areas.Admin.Models
{
    public class BookListModel
    {
        private readonly IBookService _bookService;
        public BookListModel() { }
        public BookListModel(IBookService bookService) 
        {
            _bookService = bookService;
        }
        public async Task<object> GetPagedBookAsync(DataTableHttpRequestUtility dataTable)
        {
            var data = _bookService.GetPagedBookAsync(
                dataTable.SearchText, 
                dataTable.GetSortText(new[] {"Name", "Genre", "Author" }), 
                dataTable.PageIndex, dataTable.PageSize);
            return new
            {
                recordsTotal = data.Result.total,
                recordsFiltered = data.Result.totalDispaly,
                data = from record in data.Result.records
                       select new string[]
                       {
                           record.Name,
                           record.Author,
                           record.Genre,
                           record.Id.ToString()
                       }.ToArray()

            };
        }
    }
}
