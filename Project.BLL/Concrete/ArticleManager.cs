using AutoMapper;
using Project.BLL.Abstract;
using Project.BLL.Utilities;
using Project.DAL.Abstract;
using Project.ENTITIES.Concrete;
using Project.ENTITIES.DTOs;
using Project.SHARED.Utilities.Results.Abstract;
using Project.SHARED.Utilities.Results.ComplexTypes;
using Project.SHARED.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Concrete
{
    public class ArticleManager : ManagerBase, IArticleService
    {
        public ArticleManager(IUnitOfWork unitOfWork, IMapper mapper):base(unitOfWork, mapper)
        {
        }
        public async Task<IResult> AddAsync(ArticleAddDto articleAddDto, string createdByName, int userId)
        {
            var article = Mapper.Map<Article>(articleAddDto);
            article.CreatedByName = createdByName;
            article.ModifiedByName = createdByName;
            article.UserID = userId;

            await UnitOfWork.Articles.AddAsync(article);
            await UnitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, Messages.Article.AddAsync(articleAddDto.Title));
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var articlesCount = await UnitOfWork.Articles.CountAsync();
            if(articlesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, articlesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı.", -1);
            }
        }

        public async Task<IDataResult<int>> CountByNonDeletedAsync()
        {
            var articlesCount = await UnitOfWork.Articles.CountAsync(a=>!a.IsDeleted);
            if (articlesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, articlesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı.", -1);
            }
        }

        public async Task<IResult> DeleteAsync(int articleID, string modifiedByName)
        {
            var result = await UnitOfWork.Articles.AnyAsync(a => a.ID == articleID);
            if (result)
            {
                var article = await UnitOfWork.Articles.GetAsync(a => a.ID == articleID);
                article.IsDeleted = true;
                article.IsActive = false;
                article.ModifiedByName = modifiedByName;
                article.ModifiedDate = DateTime.Now;

                await UnitOfWork.Articles.UpdateAsync(article);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Success,Messages.Article.DeleteAsync(article.Title));
            }
            return new Result(ResultStatus.Error, Messages.Article.NotFound(isPlural:false));
        }

        public async Task<IDataResult<ArticleDto>> GetAsync(int articleID)
        {
            var article = await UnitOfWork.Articles.GetAsync(a => a.ID == articleID, a => a.User, a => a.Category);
            if(article != null)
            {
                return new DataResult<ArticleDto>(ResultStatus.Success, new ArticleDto
                {
                    Article = article,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleDto>(ResultStatus.Error, Messages.Article.NotFound(isPlural:false), null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllAsync()
        {
            var articles = await UnitOfWork.Articles.GetAllAsync(null, a=>a.User, a => a.Category);
            if(articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.Article.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByCategoryAsync(int categoryID)
        {
            var result = await UnitOfWork.Categories.AnyAsync(c => c.ID == categoryID);
            if (result)
            {
                var articles = await UnitOfWork.Articles.GetAllAsync(a => a.CategoryID == categoryID && !a.IsDeleted && a.IsActive, a => a.User, a => a.Category);
                if (articles.Count > -1)
                {
                    return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                    {
                        Articles = articles,
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.Article.NotFound(isPlural: true), null);
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.Article.NotFound(isPlural: false), null);

        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAsync()
        {
            var articles = await UnitOfWork.Articles.GetAllAsync(a => !a.IsDeleted, a => a.User, a=>a.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.Article.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAndActiveAsync()
        {
            var articles = await UnitOfWork.Articles.GetAllAsync(a => !a.IsDeleted && a.IsActive, a => a.User, a => a.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.Article.NotFound(isPlural: true), null);
        }

        public async Task<IResult> HardDeleteAsync(int articleID)
        {
            var result = await UnitOfWork.Articles.AnyAsync(a => a.ID == articleID);
            if (result)
            {
                var article = await UnitOfWork.Articles.GetAsync(a => a.ID == articleID);
               

                await UnitOfWork.Articles.DeleteAsync(article);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Success,Messages.Article.HardDeleteAsync(article.Title));
            }
            return new Result(ResultStatus.Error, Messages.Article.NotFound(isPlural: false));
        }

        public async Task<IResult> UpdateAsync(ArticleUpdateDto articleUpdateDto, string modifiedByName)
        {
            var oldArticle = await UnitOfWork.Articles.GetAsync(a => a.ID == articleUpdateDto.ID);
            var article = Mapper.Map<ArticleUpdateDto, Article>(articleUpdateDto, oldArticle);
            article.ModifiedByName = modifiedByName;

            await UnitOfWork.Articles.UpdateAsync(article);
            await UnitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, Messages.Article.UpdateAsync(articleUpdateDto.Title));
        }

        public async Task<IDataResult<ArticleUpdateDto>> GetArticleUpdateDtoAsync(int articleID)
        {
            var result = await UnitOfWork.Articles.AnyAsync(a => a.ID == articleID);
            if (result)
            {
                var article = await UnitOfWork.Articles.GetAsync(a => a.ID == articleID);
                var articleUpdateDto = Mapper.Map<ArticleUpdateDto>(article);
                return new DataResult<ArticleUpdateDto>(ResultStatus.Success, articleUpdateDto);
            }
            else
            {
                return new DataResult<ArticleUpdateDto>(ResultStatus.Error, Messages.Article.NotFound(isPlural: false), null);
            }
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByDeletedAsync()
        {
            var articles = await UnitOfWork.Articles.GetAllAsync(a => a.IsDeleted, a => a.User, a => a.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.Article.NotFound(isPlural: true), null);
        }

        public async Task<IResult> UndoDeleteAsync(int articleID, string modifiedByName)
        {
            var result = await UnitOfWork.Articles.AnyAsync(a => a.ID == articleID);
            if (result)
            {
                var article = await UnitOfWork.Articles.GetAsync(a => a.ID == articleID);
                article.IsDeleted = false;
                article.IsActive = true;
                article.ModifiedByName = modifiedByName;
                article.ModifiedDate = DateTime.Now;

                await UnitOfWork.Articles.UpdateAsync(article);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Messages.Article.UndoDelete(article.Title));
            }
            return new Result(ResultStatus.Error, Messages.Article.NotFound(isPlural: false));
        }
    }
}
