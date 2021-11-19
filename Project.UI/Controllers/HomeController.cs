﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Project.BLL.Abstract;
using Project.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly AboutUsPageInfo _aboutUsPageInfo;

        public HomeController(IArticleService articleService, IOptions<AboutUsPageInfo> aboutUsPageInfo)
        {
            _articleService = articleService;
            _aboutUsPageInfo = aboutUsPageInfo.Value;
        }

        public async Task<IActionResult> Index(int? categoryID, int currentPage=1,int pageSize=5, bool isAscending = false)
        {
            var articlesResult = await (categoryID == null ? _articleService.GetAllByPagingAsync(null, currentPage,pageSize,isAscending) : _articleService.GetAllByPagingAsync(categoryID.Value, currentPage, pageSize,isAscending));
            return View(articlesResult.Data);
        }
        [HttpGet]
        public async Task<IActionResult> About()
        {
            return View(_aboutUsPageInfo);
        }
    }
}
