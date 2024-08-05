using System.ComponentModel.DataAnnotations;

namespace ArticlesApi.Models
{
    public class ArticleBase
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string title { get; set; }

        [Required]
        public string content { get; set; }

        [Required]
        public DateTime published_date { get; set; }
    }
    public class Article : ArticleBase
    {
        [Required]
        public int id { get; set; }

    }
}
