using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.Abstract;
using Project.ENTITIES.Concrete;
using Project.ENTITIES.DTOs;
using Project.SHARED.Utilities.Extensions;
using Project.SHARED.Utilities.Results.ComplexTypes;
using Project.UI.Areas.Admin.Models;
using Project.UI.Helpers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Project.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService, UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper):base(userManager, mapper, imageHelper)
        {
            _categoryService = categoryService;
        }
        [Authorize(Roles = "SuperAdmin, Category.Read")]
        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAllByNonDeletedAsync();
            //if(result.ResultStatus == ResultStatus.Success)
            //{
            //    return View(result.Data);
            //} //Bu if controlüne gerek kalmadı artık. Çünkü Category managerda ihtiyacımız olan her şeyi result'ın içine attık. Bu sayede işlem başarılı da hatalı da dönse result.Data bunu içeriyor. Bu sayede artık tek bir view'de başarılı dönerse category'leri sıralayan bir tabloyu ya da hatalı dönerse bir hata mesajını gösterebiliyoruz..
            return View(result.Data);
        }
        [Authorize(Roles = "SuperAdmin, Category.Create")]
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_CategoryAddPartial");
        }
        [Authorize(Roles = "SuperAdmin, Category.Create")]
        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddDto categoryAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.AddAsync(categoryAddDto, LoggedInUser.UserName);
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
        [Authorize(Roles = "SuperAdmin, Category.Update")]
        [HttpGet]
        public async Task<IActionResult> Update(int categoryID)
        {
            var result = await _categoryService.GetCategoryUpdateDtoAsync(categoryID);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return PartialView("_CategoryUpdatePartial", result.Data);
            }
            else
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "SuperAdmin, Category.Update")]
        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.UpdateAsync(categoryUpdateDto, LoggedInUser.UserName);
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
        [Authorize(Roles = "SuperAdmin, Category.Read")]
        public async Task<JsonResult> GetAllCategories()
        {
            var result = await _categoryService.GetAllByNonDeletedAsync();
            var categories = JsonSerializer.Serialize(result.Data, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(categories);
        }
        [Authorize(Roles = "SuperAdmin, Category.Delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(int categoryID)
        {
            var result = await _categoryService.DeleteAsync(categoryID, LoggedInUser.UserName);
            var deletedCategory = JsonSerializer.Serialize(result.Data);
            return Json(deletedCategory);
        }
        [Authorize(Roles = "SuperAdmin, Category.Read")]
        [HttpGet]
        public async Task<IActionResult> DeletedCategories()
        {
            var result = await _categoryService.GetAllByDeletedAsync();
            return View(result.Data);
        }
        [Authorize(Roles = "SuperAdmin, Category.Read")]
        [HttpGet]
        public async Task<JsonResult> GetAllDeletedCategories()
        {
            var result = await _categoryService.GetAllByDeletedAsync();
            var categories = JsonSerializer.Serialize(result.Data, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(categories);
        }
        [Authorize(Roles = "SuperAdmin, Category.Update")]
        [HttpPost]
        public async Task<JsonResult> UndoDelete(int categoryID)
        {
            var result = await _categoryService.UndoDeleteAsync(categoryID, LoggedInUser.UserName);
            var undoDeletedCategory = JsonSerializer.Serialize(result.Data);
            return Json(undoDeletedCategory);
        }
        [Authorize(Roles = "SuperAdmin, Category.Update")]
        [HttpPost]
        public async Task<JsonResult> HardDelete(int categoryID)
        {
            var result = await _categoryService.HardDeleteAsync(categoryID);
            var deletedCategory = JsonSerializer.Serialize(result);
            return Json(deletedCategory);
        }

    }
}
