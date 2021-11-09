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
    public class ArticleManager : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArticleManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IResult> Add(ArticleAddDto articleAddDto, string createdByName)
        {
            var article = _mapper.Map<Article>(articleAddDto);
            article.CreatedByName = createdByName;
            article.ModifiedByName = createdByName;
            article.UserID = 1;

            await _unitOfWork.Articles.AddAsync(article);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, Messages.Article.Add(articleAddDto.Title));
        }

        public async Task<IResult> Delete(int articleID, string modifiedByName)
        {
            var result = await _unitOfWork.Articles.AnyAsync(a => a.ID == articleID);
            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(a => a.ID == articleID);
                article.IsDeleted = true;
                article.ModifiedByName = modifiedByName;
                article.ModifiedDate = DateTime.Now;

                await _unitOfWork.Articles.UpdateAsync(article);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success,Messages.Article.Delete(article.Title));
            }
            return new Result(ResultStatus.Error, Messages.Article.NotFound(isPlural:false));
        }

        public async Task<IDataResult<ArticleDto>> Get(int articleID)
        {
            var article = await _unitOfWork.Articles.GetAsync(a => a.ID == articleID, a => a.User, a => a.Category);
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

        public async Task<IDataResult<ArticleListDto>> GetAll()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(null, a=>a.User, a => a.Category);
            if(articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.Article.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByCategory(int categoryID)
        {
            var result = await _unitOfWork.Categories.AnyAsync(c => c.ID == categoryID);
            if (result)
            {
                var articles = await _unitOfWork.Articles.GetAllAsync(a => a.CategoryID == categoryID && !a.IsDeleted && a.IsActive, a => a.User, a => a.Category);
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

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeleted()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(a => !a.IsDeleted, a => a.User, a=>a.Category);
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

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAndActive()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(a => !a.IsDeleted && a.IsActive, a => a.User, a => a.Category);
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

        public async Task<IResult> HardDelete(int articleID)
        {
            var result = await _unitOfWork.Articles.AnyAsync(a => a.ID == articleID);
            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(a => a.ID == articleID);
               

                await _unitOfWork.Articles.DeleteAsync(article);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success,Messages.Article.HardDelete(article.Title));
            }
            return new Result(ResultStatus.Error, Messages.Article.NotFound(isPlural: false));
        }

        public async Task<IResult> Update(ArticleUpdateDto articleUpdateDto, string modifiedByName)
        {
            var article = _mapper.Map<Article>(articleUpdateDto);
            article.ModifiedByName = modifiedByName;
            article.UserID = 1;

            await _unitOfWork.Articles.UpdateAsync(article);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, Messages.Article.Update(articleUpdateDto.Title));
        }
    }
}
