using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.UI.Areas.Admin.Controllers
{
    [Area("Admin")] //Bu Controller'ın area'ya ait olduğunu belirttik
    [Authorize(Roles = "Admin, Editor")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
