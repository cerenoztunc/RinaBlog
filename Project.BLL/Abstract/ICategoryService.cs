﻿using Project.ENTITIES.Concrete;
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
        Task<IDataResult<CategoryDto>> Get(int categoryID);
        Task<IDataResult<CategoryListDto>> GetAll();
        Task<IDataResult<CategoryListDto>> GetAllByNonDeleted();
        Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActive();
        Task<IResult> Add(CategoryAddDto categoryAddDto, string createdByName);
        Task<IResult> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName);
        Task<IResult> Delete(int categoryID, string modifiedByName);
        Task<IResult> HardDelete(int categoryID);
    }
}