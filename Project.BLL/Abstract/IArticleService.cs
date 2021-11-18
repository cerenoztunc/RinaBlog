using Project.ENTITIES.Concrete;
using Project.ENTITIES.DTOs;
using Project.SHARED.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Abstract
{
    public interface IArticleService
    {
        Task<IDataResult<ArticleDto>> GetAsync(int articleID);
        Task<IDataResult<ArticleUpdateDto>> GetArticleUpdateDtoAsync(int articleID);
        Task<IDataResult<ArticleListDto>> GetAllAsync();
        Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAndActiveAsync();
        Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAsync();
        Task<IDataResult<ArticleListDto>> GetAllByDeletedAsync();
        Task<IDataResult<ArticleListDto>> GetAllByCategoryAsync(int categoryID);
        Task<IDataResult<ArticleListDto>> GetAllByViewCountAsync(bool isAscending, int? takeSize);
        Task<IDataResult<ArticleListDto>> GetAllByPagingAsync(int? categoryID, int currentPage = 1, int pageSize = 5, bool isAscending = false);
        Task<IDataResult<ArticleListDto>> SearchAsync(string keyword, int currentPage = 1, int pageSize = 5, bool isAscending = false);
        Task<IResult> IncreaseViewCountAsync(int articleID);
        Task<IResult> AddAsync(ArticleAddDto articleAddDto, string createdByName, int userId);
        Task<IResult> UpdateAsync(ArticleUpdateDto articleUpdateDto, string modifiedByName);
        Task<IResult> DeleteAsync(int articleID, string modifiedByName);
        Task<IResult> UndoDeleteAsync(int articleID, string modifiedByName);
        Task<IResult> HardDeleteAsync(int articleID);
        Task<IDataResult<int>> CountAsync();
        Task<IDataResult<int>> CountByNonDeletedAsync();

    }
}
