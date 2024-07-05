using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace news_api.DTOs
{
    public class GenreDTO
    {
        public int GenreId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}