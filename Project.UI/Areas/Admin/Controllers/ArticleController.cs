using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Project.BLL.Abstract;
using Project.ENTITIES.ComplexTypes;
using Project.ENTITIES.Concrete;
using Project.ENTITIES.DTOs;
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
    public class ArticleController : BaseController
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly IToastNotification _toastNotification;
        public ArticleController(IArticleService articleService, ICategoryService categoryService, UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper, IToastNotification toastNotification) :base(userManager, mapper, imageHelper)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _toastNotification = toastNotification;
        }
        [Authorize(Roles = "SuperAdmin, Article.Read")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _articleService.GetAllByNonDeletedAsync();
            if(result.ResultStatus == ResultStatus.Success)
            return View(result.Data);
            return NotFound();
        }
        [Authorize(Roles = "SuperAdmin, Article.Create")]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var result = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            if(result.ResultStatus == ResultStatus.Success)
            {
                return View(new ArticleAddViewModel {
                    Categories = result.Data.Categories
                
                });
            }
            return NotFound();
            
        }
        [Authorize(Roles = "SuperAdmin, Article.Create")]
        [HttpPost]
        public async Task<IActionResult> Add(ArticleAddViewModel articleAddViewModel)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                var articleAddDto = Mapper.Map<ArticleAddDto>(articleAddViewModel);
                var imageResult = await ImageHelper.Upload(articleAddViewModel.Title, articleAddViewModel.ThumbnailFile, PictureTypes.Post);
                articleAddDto.Thumbnail = imageResult.Data.FullName;
                var result = await _articleService.AddAsync(articleAddDto, LoggedInUser.UserName,LoggedInUser.Id);
                if(result.ResultStatus == ResultStatus.Success)
                {
                    _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions 
                    {
                        Title = "Başarılı İşlem"
                    });
                    return RedirectToAction("Index", "Article");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            var categories = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            articleAddViewModel.Categories = categories.Data.Categories;
            return View(articleAddViewModel);

        }
        [Authorize(Roles = "SuperAdmin, Article.Update")]
        [HttpGet]
        public async Task<IActionResult> Update(int articleID)
        {
            var articleResult = await _articleService.GetArticleUpdateDtoAsync(articleID);
            var categoriesResult = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            if(articleResult.ResultStatus == ResultStatus.Success && categoriesResult.ResultStatus == ResultStatus.Success)
            {
                //ArtilceUpdateDto yu aldık ancak ArticleUpdateViewModel'a ihtiyaç olduğundan ArticleUpdateDto'yu mapping işlemleri ile ViewModel'a aktardık..
                var articleUpdateViewModel = Mapper.Map<ArticleUpdateViewModel>(articleResult.Data);
                //kategori listesinden farklı bir kategori seçilebilmesini sağladık..
                articleUpdateViewModel.Categories = categoriesResult.Data.Categories;
                return View(articleUpdateViewModel);
            }
            else
            {
                return NotFound();
            }

        }
        [Authorize(Roles = "SuperAdmin, Article.Update")]
        [HttpPost]
        public async Task<IActionResult> Update(ArticleUpdateViewModel articleUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                bool isNewThumbnailUploaded = false;
                var oldThumbnail = articleUpdateViewModel.Thumbnail;
                if(articleUpdateViewModel.ThumbnailFile != null)
                {
                    var uploadedImageResult = await ImageHelper.Upload(articleUpdateViewModel.Title, articleUpdateViewModel.ThumbnailFile,PictureTypes.Post);
                    articleUpdateViewModel.Thumbnail = uploadedImageResult.ResultStatus == ResultStatus.Success ? uploadedImageResult.Data.FullName : "postImages/bremen.jpg";
                    if(oldThumbnail != "postImages/bremen.jpg")
                    {
                        isNewThumbnailUploaded = true;
                    }
                    
                }
                var articleUpdateDto = Mapper.Map<ArticleUpdateDto>(articleUpdateViewModel);
                var result = await _articleService.UpdateAsync(articleUpdateDto, LoggedInUser.UserName);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    if (isNewThumbnailUploaded)
                    {
                        ImageHelper.Delete(oldThumbnail);
                    }
                    _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions
                    {
                        Title = "Başarılı İşlem"
                    });

                    return RedirectToAction("Index", "Article");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            var categories = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            articleUpdateViewModel.Categories = categories.Data.Categories;
            return View(articleUpdateViewModel);
        }
        [Authorize(Roles = "SuperAdmin, Article.Delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(int articleID)
        {
            var result = await _articleService.DeleteAsync(articleID, LoggedInUser.UserName);
            var articleResult = JsonSerializer.Serialize(result);
            return Json(articleResult);
        }
        [Authorize(Roles = "SuperAdmin, Article.Read")]
        [HttpGet]
        public async Task<JsonResult> GetAllArticles()
        {
            var articles = await _articleService.GetAllByNonDeletedAndActiveAsync();
            var articleResult = JsonSerializer.Serialize(articles, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(articleResult);
        }
        [Authorize(Roles = "SuperAdmin, Article.Read")]
        [HttpGet]
        public async Task<IActionResult> DeletedArticles()
        {
            var result = await _articleService.GetAllByDeletedAsync();
            return View(result.Data);
        }
        [Authorize(Roles = "SuperAdmin, Article.Read")]
        [HttpGet]
        public async Task<IActionResult> GetAllDeletedArticles()
        {
            var result = await _articleService.GetAllByDeletedAsync();
            var articles = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(articles);
        }
        [Authorize(Roles = "SuperAdmin, Article.Update")]
        [HttpPost]
        public async Task<JsonResult> UndoDelete(int articleID)
        {
            var result = await _articleService.UndoDeleteAsync(articleID, LoggedInUser.UserName);
            var undoDeletedArticleResult = JsonSerializer.Serialize(result);
            return Json(undoDeletedArticleResult);
        }
        [Authorize(Roles = "SuperAdmin, Article.Delete")]
        [HttpPost]
        public async Task<JsonResult> HardDelete(int articleID)
        {
            var result = await _articleService.HardDeleteAsync(articleID);
            var hardDeletedArticleResult = JsonSerializer.Serialize(result);
            return Json(hardDeletedArticleResult);
        }
        [Authorize(Roles = "SuperAdmin, Article.Read")]
        [HttpGet]
        public async Task<IActionResult> GetAllByView(bool isAscending, int takeSize)
        {
            var result = await _articleService.GetAllByViewCountAsync(isAscending,takeSize);
            var articles = JsonSerializer.Serialize(result.Data.Articles, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(articles);
        }



    }
}
