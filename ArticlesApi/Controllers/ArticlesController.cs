using ArticlesApi.Helpers;
using ArticlesApi.Interfaces;
using ArticlesApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ArticlesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly IHttpContextAccessor _contextAccessor;

        public ArticlesController(IArticleService articleService, IHttpContextAccessor httpContextAccessor)
        {
            _articleService = articleService;
            _contextAccessor = httpContextAccessor;
        }

        [Authorize]
        [HttpGet]
        [Route("get_articles")]
        public async Task<IActionResult> GetArticles([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _articleService.GetArticlesAsync(pageNumber, pageSize);

                if (result.Count > 0)
                {
                    var response = _articleService.Articles;
                    return Ok(response);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("get_article/{id}")]
        public async Task<IActionResult> GetArticleByIdAsync(int id)
        {
            try
            {
                var result = await _articleService.GetArticleByIdAsync(id);

                if (result != null)
                {
                    var response = _articleService.Article;
                    return Ok(response);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("add_article")]
        public async Task<IActionResult> AddArticleAsync([FromBody]ArticleBase article)
        {
            try
            {
                var result = await _articleService.AddArticleAsync(article);

                if (result != 0)
                {
                    var response = _articleService.Result_ID;

                    return Ok(response);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("update_article")]
        public async Task<IActionResult> UpdateArticleAsync(Article article)
        {
            try
            {
                var result = await _articleService.UpdateArticleAsync(article);

                if (result != 0)
                {
                    var response = _articleService.Result_ID;
                    return Ok(response);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("delete_article")]
        public async Task<IActionResult> DeleteArticleAsync(int id)
        {
            try
            {
                var result = await _articleService.DeleteArticleAsync(id);

                if (result != 0)
                {
                    var response = _articleService.Result_ID;
                    return Ok(response);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
