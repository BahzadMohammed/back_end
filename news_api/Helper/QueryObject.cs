using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace news_api.Helper
{
    public class QueryObject
    {
        public string sortBy { get; set; } = string.Empty;

        // public int? genreId { get; set; } 

        public int pageNumber { get; set; } = 1;

        // public int pageSize { get; set; } = 10;

        // public string search { get; set; } = string.Empty;
    }
}