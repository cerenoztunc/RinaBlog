using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Project.ENTITIES.DTOs;
using Project.SHARED.Utilities.Extensions;
using Project.SHARED.Utilities.Results.Abstract;
using Project.SHARED.Utilities.Results.ComplexTypes;
using Project.SHARED.Utilities.Results.Concrete;
using Project.UI.Helpers.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Project.UI.Helpers.Concrete
{
    public class ImageHelper : IImageHelper
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _wwwroot;
        private readonly string imgFolder = "img";
        public ImageHelper(IWebHostEnvironment env)
        {
            _env = env;
            _wwwroot = _env.WebRootPath;
        }

        public IDataResult<ImageDeletedDto> Delete(string pictureName)
        {
            var fileToDelete = Path.Combine($"{_wwwroot}/{imgFolder}/", pictureName);
            if (System.IO.File.Exists(fileToDelete))
            {
                var fileInfo = new FileInfo(fileToDelete);
                var imageDeletedDto = new ImageDeletedDto //burada instance aldık çünkü kullandığımız FileInfo yapısı istediğimiz bilgileri elinde tutabilmek için programda o dosyanın bulunmasını mecbur kılıyor. Dosya bilgisini elde ettikten sonra bir aşağıda delete ettiğimizden return ederken oluşturacağımız yeni imageDeletedDto içindeki bilgilere exception fırlatır. Çünkü artık o dosya bilgisine erişemeyecektir. Bu yüzden silmeden önce instance alıp return ederken bunu vermeliyiz...
                {
                    FullName = pictureName,
                    Extension = fileInfo.Extension,
                    Path = fileInfo.FullName,
                    Size = fileInfo.Length
                };
                System.IO.File.Delete(fileToDelete);
                return new DataResult<ImageDeletedDto>(ResultStatus.Success, imageDeletedDto);
            }
            else
            {
                return new DataResult<ImageDeletedDto>(ResultStatus.Error, $"Böyle bir resim bulunamadı.", null);
            }
        }

        public async Task<IDataResult<ImageUploadedDto>> UploadUserImage(string userName, IFormFile pictureFile, string folderName="userImages")
        {
            if (!Directory.Exists($"{_wwwroot}/{imgFolder}/{folderName}"))
            {
                Directory.CreateDirectory($"{_wwwroot}/{imgFolder}/{folderName}");
            }
            string oldFileName = Path.GetFileNameWithoutExtension(pictureFile.FileName);

            //.png
            string fileExtension = Path.GetExtension(pictureFile.FileName);
            DateTime dateTime = DateTime.Now;
            //CerenOztunc_678_5_56_12_3_49_2021.png
            string newFileName = $"{userName}_{dateTime.FullDateAndTimeStringWithUnderscore()}{fileExtension}";
            //dosya adını nereye kaydedeceğimiz..
            var path = Path.Combine($"{_wwwroot}/{imgFolder}/{folderName}", newFileName);

            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await pictureFile.CopyToAsync(stream);
            }
            return new DataResult<ImageUploadedDto>(ResultStatus.Success, $"{userName} adlı kullanıcının resmi başarıyla yüklenmiştir.", 
                new ImageUploadedDto 
                {
                    FullName = $"{folderName}/{newFileName}",
                    OldName = oldFileName,
                    Extension = fileExtension,
                    Path = path,
                    Size = pictureFile.Length
                });
        }
    }
}
