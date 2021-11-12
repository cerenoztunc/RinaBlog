using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.ENTITIES.Concrete;
using Project.UI.Helpers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.UI.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        //protected olması sadece basecontroller'dan türeyen sınıfların da bu metodu kullanabilmesi için
        //get olması farklı bir sınıfın bunları değiştirmesini istemiyoruz. UserManager'ı kullanalım ancak UserManager'ı değiştirecek herhangi bir set işlemi yapmamalıyız..
        protected UserManager<User> UserManager { get; }
        protected IMapper Mapper { get; }
        protected IImageHelper ImageHelper { get; }
        //giriş yapmış kullanıcı bilgisini bu şekilde elde edeceğiz..
        protected User LoggedInUser => UserManager.GetUserAsync(HttpContext.User).Result;

        public BaseController(UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper)
        {
            UserManager = userManager;
            Mapper = mapper;
            ImageHelper = imageHelper;
        }

    }
}
