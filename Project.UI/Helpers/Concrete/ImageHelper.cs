using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Project.ENTITIES.ComplexTypes;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project.UI.Helpers.Concrete
{
    public class ImageHelper : IImageHelper
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _wwwroot;
        private const string imgFolder = "img";
        private const string userImageFolder = "userImages";
        private const string postImagesFolder = "postImages";
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

        public string Upload(string name, IFormFile pictureFile, PictureTypes pictureTypes, string folderName=null)
        {
            //folderName null gelirse resim tipine göre klasör adı ataması yapılır..
            folderName ??= pictureTypes == PictureTypes.User ? userImageFolder : postImagesFolder;

            //folderName değişkeni ile gelen klasör adı sistemde yoksa yeni klasör oluştuurlur...
            if (!Directory.Exists($"{_wwwroot}/{imgFolder}/{folderName}"))
            {
                Directory.CreateDirectory($"{_wwwroot}/{imgFolder}/{folderName}");
            }
            //resmin yüklenme sırasındaki ilk adı oldFilename adlı değişkene atanır..
            string oldFileName = Path.GetFileNameWithoutExtension(pictureFile.FileName);

            //resmin uzantısı (.png, jpg vb.) fileExtension adlı değişkene atanır..
            string fileExtension = Path.GetExtension(pictureFile.FileName);

            //resmin adında istenmeyen karakterlerin kaldırılması)
            Regex regex = new Regex("[*'\",._&#^@]");
            name = regex.Replace(name, string.Empty);

            DateTime dateTime = DateTime.Now;

            //Parametreyle gelen değerler kullanılarak yeni bir resim adı oluşturulur
            //CerenOztunc_678_5_56_12_3_49_2021.png
            string newFileName = $"{name}_{dateTime.FullDateAndTimeStringWithUnderscore()}{fileExtension}";

            //dosya adını nereye kaydedeceğimiz..
            var path = Path.Combine($"{_wwwroot}/{imgFolder}/{folderName}", newFileName);

            //sistem için oluşturulan yeni dosya yoluna resim kopyalanır..
            using (var stream = new FileStream(path, FileMode.Create))
            {
                pictureFile.CopyToAsync(stream);
            }

            //resim tipine göre kullanıcı mesajı oluşturulur..
            string message = pictureTypes == PictureTypes.User ? $"{name} adlı kullanıcının resmi başarıyla yüklenmiştir." : $"{name} adlı makalenin resmi başarıyla yüklenmiştir.";

            return $"{folderName}/{newFileName}";
        }
    }
}
