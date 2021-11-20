using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NToastNotify;
using Project.BLL.Abstract;
using Project.ENTITIES.Concrete;
using Project.ENTITIES.DTOs;
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
        private readonly IMailService _mailService;
        private readonly IToastNotification _toastNotification;
        public HomeController(IArticleService articleService, IOptions<AboutUsPageInfo> aboutUsPageInfo, IMailService mailService, IToastNotification toastNotification)
        {
            _articleService = articleService;
            _aboutUsPageInfo = aboutUsPageInfo.Value;
            _mailService = mailService;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index(int? categoryID, int currentPage=1,int pageSize=5, bool isAscending = false)
        {
            var articlesResult = await (categoryID == null ? _articleService.GetAllByPagingAsync(null, currentPage,pageSize,isAscending) : _articleService.GetAllByPagingAsync(categoryID.Value, currentPage, pageSize,isAscending));
            return View(articlesResult.Data);
        }
        [HttpGet]
        public IActionResult About()
        {
            return View(_aboutUsPageInfo);
        }
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(EmailSendDto emailSendDto)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                var result = _mailService.SendContactEmail(emailSendDto);
                _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions
                {
                    Title = "Başarılı İşlem!"
                });
                return View();
            }
            return View(emailSendDto);
        }
    }
}
