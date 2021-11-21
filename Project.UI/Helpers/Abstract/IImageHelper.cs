using Microsoft.AspNetCore.Http;
using Project.ENTITIES.ComplexTypes;
using Project.ENTITIES.DTOs;
using Project.SHARED.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.UI.Helpers.Abstract
{
    public interface IImageHelper
    {
        string Upload(string name, IFormFile pictureFile, PictureTypes pictureTypes, string folderName = null);
        IDataResult<ImageDeletedDto> Delete(string pictureName);
    }
}
