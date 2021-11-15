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
    public class CommentManager: ManagerBase, ICommentService
    {
        public CommentManager(IUnitOfWork unitOfWork, IMapper mapper):base(unitOfWork,mapper)
        {
        }
        public async Task<IDataResult<CommentDto>> GetAsync(int commentID)
        {
            var comment = await UnitOfWork.Comments.GetAsync(c => c.ID == commentID);
            if (comment != null)
            {
                return new DataResult<CommentDto>(ResultStatus.Success, new CommentDto
                {
                    Comment = comment,
                });
            }
            return new DataResult<CommentDto>(ResultStatus.Error, Messages.Comment.NotFound(isPlural: false), new CommentDto
            {
                Comment = null,
            });
        }

        public async Task<IDataResult<CommentUpdateDto>> GetCommentUpdateDtoAsync(int commentID)
        {
            var result = await UnitOfWork.Comments.AnyAsync(c => c.ID == commentID);
            if (result)
            {
                var comment = await UnitOfWork.Comments.GetAsync(c => c.ID == commentID);
                var commentUpdateDto = Mapper.Map<CommentUpdateDto>(comment);
                return new DataResult<CommentUpdateDto>(ResultStatus.Success, commentUpdateDto);
            }
            else
            {
                return new DataResult<CommentUpdateDto>(ResultStatus.Error, Messages.Comment.NotFound(isPlural: false), null);
            }
        }

        public async Task<IDataResult<CommentListDto>> GetAllAsync()
        {
            var comments = await UnitOfWork.Comments.GetAllAsync(null, c=>c.Article);
            if (comments.Count > -1)
            {
                return new DataResult<CommentListDto>(ResultStatus.Success, new CommentListDto
                {
                    Comments = comments,
                });
            }
            return new DataResult<CommentListDto>(ResultStatus.Error, Messages.Comment.NotFound(isPlural: true), new CommentListDto
            {
                Comments = null,
            });
        }

        public async Task<IDataResult<CommentListDto>> GetAllByDeletedAsync()
        {
            var comments = await UnitOfWork.Comments.GetAllAsync(c => c.IsDeleted, c => c.Article);
            if (comments.Count > -1)
            {
                return new DataResult<CommentListDto>(ResultStatus.Success, new CommentListDto
                {
                    Comments = comments,
                });
            }
            return new DataResult<CommentListDto>(ResultStatus.Error, Messages.Comment.NotFound(isPlural: true), new CommentListDto
            {
                Comments = null,
            });
        }

        public async Task<IDataResult<CommentListDto>> GetAllByNonDeletedAsync()
        {
            var comments = await UnitOfWork.Comments.GetAllAsync(c => !c.IsDeleted, c => c.Article);
            if (comments.Count > -1)
            {
                return new DataResult<CommentListDto>(ResultStatus.Success, new CommentListDto
                {
                    Comments = comments,
                });
            }
            return new DataResult<CommentListDto>(ResultStatus.Error, Messages.Comment.NotFound(isPlural: true), new CommentListDto
            {
                Comments = null,
            });
        }

        public async Task<IDataResult<CommentListDto>> GetAllByNonDeletedAndActiveAsync()
        {
            var comments = await UnitOfWork.Comments.GetAllAsync(c => !c.IsDeleted && c.IsActive, c => c.Article);
            if (comments.Count > -1)
            {
                return new DataResult<CommentListDto>(ResultStatus.Success, new CommentListDto
                {
                    Comments = comments,
                });
            }
            return new DataResult<CommentListDto>(ResultStatus.Error, Messages.Comment.NotFound(isPlural: true), new CommentListDto
            {
                Comments = null,
            });
        }

        public async Task<IDataResult<CommentDto>> AddAsync(CommentAddDto commentAddDto)
        {
            var comment = Mapper.Map<Comment>(commentAddDto);
            var addedComment = await UnitOfWork.Comments.AddAsync(comment);
            await UnitOfWork.SaveAsync();
            return new DataResult<CommentDto>(ResultStatus.Success, Messages.Comment.Add(commentAddDto.CreatedByName), new CommentDto
            {
                Comment = addedComment,
            });
        }

        public async Task<IDataResult<CommentDto>> UpdateAsync(CommentUpdateDto commentUpdateDto, string modifiedByName)
        {
            var oldComment = await UnitOfWork.Comments.GetAsync(c => c.ID == commentUpdateDto.ID);
            var comment = Mapper.Map<CommentUpdateDto, Comment>(commentUpdateDto, oldComment);
            comment.ModifiedByName = modifiedByName;
            var updatedComment = await UnitOfWork.Comments.UpdateAsync(comment);
            updatedComment.Article = await UnitOfWork.Articles.GetAsync(a => a.ID == updatedComment.ArticleID);
            await UnitOfWork.SaveAsync();
            return new DataResult<CommentDto>(ResultStatus.Success, Messages.Comment.Update(comment.CreatedByName), new CommentDto
            {
                Comment = updatedComment,
            });
        }

        public async Task<IDataResult<CommentDto>> DeleteAsync(int commentID, string modifiedByName)
        {
            var comment = await UnitOfWork.Comments.GetAsync(c => c.ID == commentID);
            if (comment != null)
            {
                comment.IsDeleted = true;
                comment.IsActive = false;
                comment.ModifiedByName = modifiedByName;
                comment.ModifiedDate = DateTime.Now;
                var deletedComment = await UnitOfWork.Comments.UpdateAsync(comment);
                await UnitOfWork.SaveAsync();
                return new DataResult<CommentDto>(ResultStatus.Success, Messages.Comment.Delete(deletedComment.CreatedByName), new CommentDto
                {
                    Comment = deletedComment,
                });
            }
            return new DataResult<CommentDto>(ResultStatus.Error, Messages.Comment.NotFound(isPlural: false), new CommentDto
            {
                Comment = null,
            });
        }

        public async Task<IResult> HardDeleteAsync(int commentID)
        {
            var comment = await UnitOfWork.Comments.GetAsync(c => c.ID == commentID);
            if (comment != null)
            {
                await UnitOfWork.Comments.DeleteAsync(comment);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Messages.Comment.HardDelete(comment.CreatedByName));
            }
            return new Result(ResultStatus.Error, Messages.Comment.NotFound(isPlural: false));
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var commentsCount = await UnitOfWork.Comments.CountAsync();
            if (commentsCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, commentsCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı.", -1);
            }
        }

        public async Task<IDataResult<int>> CountByNonDeletedAsync()
        {
            var commentsCount = await UnitOfWork.Comments.CountAsync(c => !c.IsDeleted);
            if (commentsCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, commentsCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı.", -1);
            }
        }

        public async Task<IDataResult<CommentDto>> ApproveAsync(int commentID, string modifiedByName)
        {
            var comment = await UnitOfWork.Comments.GetAsync(c => c.ID == commentID, c => c.Article);
            if(comment != null)
            {
                comment.IsActive = true;
                comment.ModifiedByName = modifiedByName;
                comment.ModifiedDate = DateTime.Now;
                var updatedComment = await UnitOfWork.Comments.UpdateAsync(comment);
                await UnitOfWork.SaveAsync();
                return new DataResult<CommentDto>(ResultStatus.Success,Messages.Comment.Approve(commentID), new CommentDto
                {
                    Comment = updatedComment
                });
            }
            return new DataResult<CommentDto>(ResultStatus.Error, Messages.Comment.NotFound(false), null);
        }

        public async Task<IDataResult<CommentDto>> UndoDeleteAsync(int commentID, string modifiedByName)
        {
            var comment = await UnitOfWork.Comments.GetAsync(c => c.ID == commentID);
            if (comment != null)
            {
                comment.IsDeleted = false;
                comment.IsActive = true;
                comment.ModifiedByName = modifiedByName;
                comment.ModifiedDate = DateTime.Now;
                var deletedComment = await UnitOfWork.Comments.UpdateAsync(comment);
                await UnitOfWork.SaveAsync();
                return new DataResult<CommentDto>(ResultStatus.Success, Messages.Comment.UndoDelete(deletedComment.CreatedByName), new CommentDto
                {
                    Comment = deletedComment,
                });
            }
            return new DataResult<CommentDto>(ResultStatus.Error, Messages.Comment.NotFound(isPlural: false), new CommentDto
            {
                Comment = null,
            });
        }
    }
}
