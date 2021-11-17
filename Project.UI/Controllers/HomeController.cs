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

        public async Task<IActionResult> Index(int? categoryID, int currentPage=1,int pageSize=5, bool isAscending = false)
        {
            var articlesResult = await (categoryID == null ? _articleService.GetAllByPagingAsync(null, currentPage,pageSize,isAscending) : _articleService.GetAllByPagingAsync(categoryID.Value, currentPage, pageSize,isAscending));
            return View(articlesResult.Data);
        }
    }
}
