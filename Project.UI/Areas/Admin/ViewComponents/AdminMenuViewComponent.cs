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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User); //Hangi kullanıcı giriş yapmış elde ettik..
            var roles = await _userManager.GetRolesAsync(user);
            if (user == null) return Content("Kullanıcı Bulunamadı!");
            if (roles == null) return Content("Rol Bulunamadı!");
            return View(new UserWithRolesViewModel
            {
                User = user,
                Roles = roles
            });

        }
    }
}
