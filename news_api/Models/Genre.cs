using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace news_api.model
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        public ICollection<News> News { get; set; } = new List<News>();
    }
}