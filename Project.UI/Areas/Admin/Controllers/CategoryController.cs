using Microsoft.AspNetCore.Mvc;
using Project.BLL.Abstract;
using Project.SHARED.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAll();
            //if(result.ResultStatus == ResultStatus.Success)
            //{
            //    return View(result.Data);
            //} //Bu if controlüne gerek kalmadı artık. Çünkü Category managerda ihtiyacımız olan her şeyi result'ın içine attık. Bu sayede işlem başarılı da hatalı da dönse result.Data bunu içeriyor. Bu sayede artık tek bir view'de başarılı dönerse category'leri sıralayan bir tabloyu ya da hatalı dönerse bir hata mesajını gösterebiliyoruz..
            return View(result.Data);
        }
        public IActionResult Add()
        {
            return PartialView("_CategoryAddPartial");
        }
    }
}
