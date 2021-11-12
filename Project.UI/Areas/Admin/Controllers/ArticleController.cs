using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
using System.Threading.Tasks;

namespace Project.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : BaseController
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        public ArticleController(IArticleService articleService, ICategoryService categoryService, UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper):base(userManager, mapper, imageHelper)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _articleService.GetAllByNonDeletedAsync();
            if(result.ResultStatus == ResultStatus.Success)
            return View(result.Data);
            return NotFound();
        }
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
                    TempData.Add("SuccessMessage", result.Message);
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
                    articleUpdateViewModel.Thumbnail = uploadedImageResult.ResultStatus == ResultStatus.Success ? uploadedImageResult.Data.FullName : "postImages/profile.jpg";
                    if(oldThumbnail != "postImages/profile.jpg")
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
                    TempData.Add("SuccessMessage", result.Message);
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

    }
}
