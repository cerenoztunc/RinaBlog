using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.ENTITIES.Concrete;
using Project.ENTITIES.DTOs;
using Project.SHARED.Utilities.Extensions;
using Project.SHARED.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Project.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;

        public UserController(UserManager<User> userManager, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(new UserListDto {
                Users = users,
                ResultStatus = ResultStatus.Success
            
            });
        }
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_UserAddPartial");
        }
        public async Task<string> ImageUpload(UserAddDto userAddDto)
        {
            // ~/img/user.Picture
            string wwwroot = _env.WebRootPath;

            //cerenPP
            //string userFileName = Path.GetFileNameWithoutExtension(userAddDto.Picture.FileName);

            //.png
            string fileExtension = Path.GetExtension(userAddDto.Picture.FileName);
            DateTime dateTime = DateTime.Now;
            //CerenOztunc_678_5_56_12_3_49_2021.png
            string fileName = $"{userAddDto.UserName}_{dateTime.FullDateAndTimeStringWithUnderscore()}{fileExtension}";
            //dosya adını nereye kaydedeceğimiz..
            var path = Path.Combine($"{wwwroot}/img", fileName);

            await using(var stream = new FileStream(path, FileMode.Create))
            {
                await userAddDto.Picture.CopyToAsync(stream);
            }
            return fileName;
        }
    }
}
