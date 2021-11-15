using Project.ENTITIES.DTOs;
using Project.SHARED.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Abstract
{
    public interface ICommentService
    {
        Task<IDataResult<CommentDto>> GetAsync(int commentID);
        Task<IDataResult<CommentUpdateDto>> GetCommentUpdateDtoAsync(int commentID);
        Task<IDataResult<CommentListDto>> GetAllAsync();
        Task<IDataResult<CommentListDto>> GetAllByDeletedAsync();
        Task<IDataResult<CommentListDto>> GetAllByNonDeletedAsync();
        Task<IDataResult<CommentListDto>> GetAllByNonDeletedAndActiveAsync();
        Task<IDataResult<CommentDto>> AddAsync(CommentAddDto commentAddDto);
        Task<IDataResult<CommentDto>> ApproveAsync(int commentID, string modifiedByName);
        Task<IDataResult<CommentDto>> UpdateAsync(CommentUpdateDto commentUpdateDto, string modifiedByName);
        Task<IDataResult<CommentDto>> DeleteAsync(int commentID, string modifiedByName);
        Task<IDataResult<CommentDto>> UndoDeleteAsync(int commentID, string modifiedByName);
        Task<IResult> HardDeleteAsync(int commentID);
        Task<IDataResult<int>> CountAsync();
        Task<IDataResult<int>> CountByNonDeletedAsync();

    }
}
