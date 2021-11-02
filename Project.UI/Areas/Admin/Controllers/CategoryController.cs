using Microsoft.AspNetCore.Mvc;
using Project.BLL.Abstract;
using Project.ENTITIES.DTOs;
using Project.SHARED.Utilities.Extensions;
using Project.SHARED.Utilities.Results.ComplexTypes;
using Project.UI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
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
            var result = await _categoryService.GetAllByNonDeleted();
            //if(result.ResultStatus == ResultStatus.Success)
            //{
            //    return View(result.Data);
            //} //Bu if controlüne gerek kalmadı artık. Çünkü Category managerda ihtiyacımız olan her şeyi result'ın içine attık. Bu sayede işlem başarılı da hatalı da dönse result.Data bunu içeriyor. Bu sayede artık tek bir view'de başarılı dönerse category'leri sıralayan bir tabloyu ya da hatalı dönerse bir hata mesajını gösterebiliyoruz..
            return View(result.Data);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_CategoryAddPartial");
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddDto categoryAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.Add(categoryAddDto, "Ceren Oztunc");
                if(result.ResultStatus == ResultStatus.Success)
                {
                    var categoryAddAjaxModel = JsonSerializer.Serialize(new CategoryAddAjaxViewModel
                    {
                        CategoryDto = result.Data,
                        CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial", categoryAddDto) //buna bir daha güncellenmiş değeri göndermek için ihtiyacımız var..
                    });
                    return Json(categoryAddAjaxModel);
                }
                
            }
            var categoryAddAjaxErrorModel = JsonSerializer.Serialize(new CategoryAddAjaxViewModel
            {
                CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial", categoryAddDto) //hatalı input'larla ilgili hata mesajlarını gösterecek..
            });
            return Json(categoryAddAjaxErrorModel);

        }
        [HttpGet]
        public async Task<IActionResult> Update(int categoryID)
        {
            var result = await _categoryService.GetCategoryUpdateDto(categoryID);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return PartialView("_CategoryUpdatePartial", result.Data);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.Update(categoryUpdateDto, "Ceren Oztunc");
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var categoryUpdateAjaxModel = JsonSerializer.Serialize(new CategoryUpdateAjaxViewModel
                    {
                        CategoryDto = result.Data,
                        CategoryUpdatePartial = await this.RenderViewToStringAsync("_CategoryUpdatePartial", categoryUpdateDto) //buna bir daha güncellenmiş değeri göndermek için ihtiyacımız var..
                    });
                    return Json(categoryUpdateAjaxModel);
                }

            }
            var categoryUpdateAjaxErrorModel = JsonSerializer.Serialize(new CategoryUpdateAjaxViewModel
            {
                CategoryUpdatePartial = await this.RenderViewToStringAsync("_CategoryUpdatePartial", categoryUpdateDto) //hatalı input'larla ilgili hata mesajlarını gösterecek..
            });
            return Json(categoryUpdateAjaxErrorModel);

        }

        public async Task<JsonResult> GetAllCategories()
        {
            var result = await _categoryService.GetAllByNonDeleted();
            var categories = JsonSerializer.Serialize(result.Data, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(categories);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int categoryID)
        {
            var result = await _categoryService.Delete(categoryID, "Ceren Öztunç");
            var deletedCategory = JsonSerializer.Serialize(result.Data);
            return Json(deletedCategory);
        }
    }
}
