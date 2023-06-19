using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Utilities
{
    public class DataTableHttpRequestUtility
    {
        private HttpRequest _request;
        public DataTableHttpRequestUtility(HttpRequest request) 
        {
            _request = request;
        }

        public int Start
        {
            get
            {
                return int.Parse(RequestData.Where(x=>x.Key=="start").FirstOrDefault().Value);
            }
        }

        public int Length
        {
            get
            {
                return int.Parse(RequestData.Where(x=>x.Key== "length").FirstOrDefault().Value);
            }
        }
        public string SearchText
        {
            get
            {
                return RequestData.Where(x=>x.Key == "search[value]").FirstOrDefault().Value;
            }
        }

        public int PageIndex
        {
            get
            {
                if (Length > 1)
                {
                    return (Start/Length)+1;
                }
                else
                {
                    return 1;
                }
            }
        }

        public int PageSize
        {
            get
            {
                if (Length > 0)
                    return Length;
                else return 10;
            }
        }
        public string GetSortText(string[] columnNames)
        {
            StringBuilder sortedText = new StringBuilder();
            for(int columnIndex=0; columnIndex<columnNames.Length; columnIndex++)
            {
                if (RequestData.Any(x => x.Key == $"order[{columnIndex}][column]"))
                {
                    if(sortedText.Length> 0)
                    {
                        sortedText.Append(",");
                    }
                    var columnValue = RequestData.Where(x => x.Key == $"order[{columnIndex}][column]").FirstOrDefault();
                    var directionValue = RequestData.Where(x => x.Key == $"order[{columnIndex}][dir]").FirstOrDefault();

                    var column = int.Parse(columnValue.Value.ToArray()[0]);
                    var dir = directionValue.Value.ToArray()[0];
                    var columnWithDir = $"{columnNames[column]} {(dir == "asc" ? "asc":"desc")}";
                    sortedText.Append(columnWithDir);
                }
            }
            return sortedText.ToString();
        }
        private IEnumerable<KeyValuePair<string, StringValues>> RequestData
        {
            get
            {
                var method = _request.Method.ToLower();
                if (method == "get")
                    return _request.Query;
                if (method == "post")
                    return _request.Form;
                else throw new InvalidOperationException("Method not support. Use Get or Post Method");
            
            }
        }
    }
}
