using ArticlesApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ArticlesApi.Interfaces
{
    public interface IArticleService
    {
        List<Article> Articles { get; set; }
        ArticleBase Article { get; set; }
        int Result_ID { get; set; }

        Task<List<Article>> GetArticlesAsync(int pageNumber, int pageSize);
        Task<ArticleBase> GetArticleByIdAsync(int id);
        Task<int> AddArticleAsync(ArticleBase article);
        Task<int> UpdateArticleAsync(Article article);
        Task<int> DeleteArticleAsync(int id);

    }
}
