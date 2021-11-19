using Microsoft.AspNetCore.Mvc;
using Project.BLL.Abstract;
using Project.ENTITIES.ComplexTypes;
using Project.SHARED.Utilities.Results.ComplexTypes;
using Project.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.UI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }
        [HttpGet]
        public async Task<IActionResult> Search(string keyword, int currentPage=1, int pageSize=5,bool isAscending=false)
        {
            var searchResult = await _articleService.SearchAsync(keyword, currentPage, pageSize, isAscending);
            if (searchResult.ResultStatus == ResultStatus.Success)
            {
                return View(new ArticleSearchViewModel
                {
                    ArticleListDto = searchResult.Data,
                    Keyword = keyword
                });
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int articleID)
        {
            var articleResult = await _articleService.GetAsync(articleID);
            if(articleResult.ResultStatus == ResultStatus.Success)
            {
                var userArticles = await _articleService.GetAllByUserIdOnFilter(articleResult.Data.Article.UserID, FilterBy.Category, OrderBy.Date, false, 10, articleResult.Data.Article.CategoryID, DateTime.Now, DateTime.Now, 0, 99999, 0, 99999);
                await _articleService.IncreaseViewCountAsync(articleID);
                return View(new ArticleDetailViewModel
                {
                    ArticleDto = articleResult.Data,
                    ArticleDetailRightSideBarViewModel = new ArticleDetailRightSideBarViewModel
                    {
                        ArticleListDto = userArticles.Data,
                        Header = "Kullanıcının Aynı Kategori Üzerindeki En Çok Okunan Makaleleri",
                        User = articleResult.Data.Article.User
                    }

                });
            }
            return NotFound();
        }
    }
}
