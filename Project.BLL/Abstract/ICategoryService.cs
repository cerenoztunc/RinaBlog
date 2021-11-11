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
    public interface ICategoryService
    {
        Task<IDataResult<CategoryDto>> GetAsync(int categoryID);
        /// <summary>
        /// Verilen ID parametresine ait kategorinin CategoryUpdateDto temsilini geriye döner.
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns>Asenkron bir operasyon ile Task olarak işlem sonucu DataResult tipinde geriye döner.</returns>
        Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDtoAsync(int categoryID);
        Task<IDataResult<CategoryListDto>> GetAllAsync();
        Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAsync();
        Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActiveAsync();
        /// <summary>
        /// Verilen CategoryAddDto ve CreatedByName parametrelerine ait bilgiler ile yeni bir Category ekler.
        /// </summary>
        /// <param name="categoryAddDto"></param>
        /// <param name="createdByName"></param>
        /// <returns>Asenkron bir operasyon ile Task olarak ekleme işleminin sonucunu DataResult tipinde döner.</returns>
        Task<IDataResult<CategoryDto>> AddAsync(CategoryAddDto categoryAddDto, string createdByName);
        Task<IDataResult<CategoryDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto, string modifiedByName);
        Task<IDataResult<CategoryDto>> DeleteAsync(int categoryID, string modifiedByName);
        Task<IResult> HardDeleteAsync(int categoryID);
        Task<IDataResult<int>> CountAsync();
        Task<IDataResult<int>> CountByNonDeletedAsync();
    }
}
