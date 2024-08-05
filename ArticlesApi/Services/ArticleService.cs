using ArticlesApi.Helpers;
using ArticlesApi.Interfaces;
using ArticlesApi.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace ArticlesApi.Services
{
    public class ArticleService : IArticleService
    {

        public List<Article> Articles { get; set; }
        public ArticleBase Article { get; set; }
        public int Result_ID { get; set; } = 0;

        public async Task<int> AddArticleAsync(ArticleBase article)
        {
            string sql = $@"INSERT INTO dbo.Articles(title, content, published_date) 
                            VALUES (@title, @content, @published_date)";
            
            try
            {
                using (var conn = new SqlConnection(Globals.conn))
                {
                    this.Result_ID = await conn.ExecuteScalarAsync<int>(sql, new
                    {
                        title = article.title,
                        content = article.content,
                        published_date = article.published_date
                    });
                }
                return Result_ID;
            }
            catch (Exception)
            {
                return Result_ID;
            }
        }

        public async Task<int> DeleteArticleAsync(int id)
        {
            string sql = $@"DELETE FROM dbo.Articles WHERE article_id = @ID";

            try
            {
                using (var conn = new SqlConnection(Globals.conn))
                {
                    this.Result_ID = await conn.ExecuteScalarAsync<int>(sql, new
                    {
                        ID = id
                    });
                }
                return Result_ID;
            }
            catch (Exception)
            {
                return Result_ID;
            }
        }

        public async Task<List<Article>> GetArticlesAsync(int pageNumber, int pageSize)
        {
            string sql = $@"SELECT * FROM dbo.Articles
                    ORDER BY article_id
                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY";

            var offset = (pageNumber - 1) * pageSize;

            this.Articles = new List<Article>();

            try
            {
                using (var conn = new SqlConnection(Globals.conn))
                {
                    using (var reader = (DbDataReader)conn.ExecuteReader(sql, new { Offset = offset, PageSize = pageSize }))
                    {
                        while (await reader.ReadAsync())
                        {

                            Article article = new Article();
                            
                            article.id = CheckReader.GetValue(reader,"article_id", article.id);
                            article.title = CheckReader.GetValue(reader, "title", article.title);
                            article.content = CheckReader.GetValue(reader, "content", article.content);
                            article.published_date = CheckReader.GetValue(reader, "published_date", article.published_date);

                            Articles.Add(article);
                        }
                    }
                }
                return this.Articles;
            }
            catch (Exception)
            {
                return this.Articles;
            }
        }

        public async Task<ArticleBase> GetArticleByIdAsync(int id)
        {
            string sql = $@"SELECT * FROM dbo.Articles WHERE article_id = @ID";

            this.Article = new ArticleBase();

            try
            {
                using (var conn = new SqlConnection(Globals.conn))
                {
                    using (var reader = (DbDataReader)conn.ExecuteReader(sql, new { ID = id}))
                    {
                        while (await reader.ReadAsync())
                        {

                            ArticleBase article = new ArticleBase();

                            article.title = CheckReader.GetValue(reader, "title", article.title);
                            article.content = CheckReader.GetValue(reader, "content", article.content);
                            article.published_date = CheckReader.GetValue(reader, "published_date", article.published_date);

                            Article = article ;
                        }
                    }
                }
                return this.Article;
            }
            catch (Exception)
            {
                return this.Article;
            }
        }

        public async Task<int> UpdateArticleAsync(Article article)
        {
            string sql = $@"UPDATE dbo.Articles SET title = @Title,
                                                    content = @Content,
                                                    published_date = @Date
                            WHERE article_id = @ID";
            try
            {
                using (var conn = new SqlConnection(Globals.conn))
                {
                    this.Result_ID = await conn.ExecuteScalarAsync<int>(sql, new
                    {
                        ID = article.id,
                        Title = article.title,
                        Content = article.content,
                        Date = article.published_date
                    });
                }
                return Result_ID;
            }
            catch (Exception)
            {
                return Result_ID;
            }
        }
    }
}
