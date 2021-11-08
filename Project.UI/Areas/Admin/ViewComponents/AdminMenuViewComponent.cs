using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Project.ENTITIES.Concrete;
using Project.UI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.UI.Areas.Admin.ViewComponents
{
    public class AdminMenuViewComponent:ViewComponent
    {
        private readonly UserManager<User> _userManager;

        public AdminMenuViewComponent(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public ViewViewComponentResult Invoke()
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result; //Hangi kullanıcı giriş yapmış elde ettik..
            var roles = _userManager.GetRolesAsync(user).Result;
            return View(new UserWithRolesViewModel
            {
                User = user,
                Roles = roles
            });

        }
    }
}
