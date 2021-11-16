using Microsoft.AspNetCore.Mvc;
using Project.BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;

        public HomeController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public async Task<IActionResult> Index(int? categoryID)
        {
            var articlesResult = await (categoryID == null ? _articleService.GetAllByNonDeletedAndActiveAsync() : _articleService.GetAllByCategoryAsync(categoryID.Value));
            return View(articlesResult.Data);
        }
    }
}
