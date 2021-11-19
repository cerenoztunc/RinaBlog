using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Hosting;
using Project.SHARED.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.UI.Filters
{
    public class MvcExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _environment;
        private readonly IModelMetadataProvider _metadataProvider;
        public MvcExceptionFilter(IHostEnvironment environment, IModelMetadataProvider metadataProvider = null)
        {
            _environment = environment;
            _metadataProvider = metadataProvider;
        }

        public void OnException(ExceptionContext context)
        {
            if (_environment.IsDevelopment()) //geliştirme aşamasında olduğumuz için bunu verdik. Bittiğinde IsProduction yazılmalı..
            {
                context.ExceptionHandled = true; //hata kısmının ele alınıp bununla ilgili bir işlem yapıldığını ifade eder..
                var mvcModelError = new MvcErrorModel
                {
                    Message = $"Üzgünüz, işleminiz sırasında beklenmedik bir hata oluştu. Sorunu en kısa sürede çözeceğiz..."
                };
                var result = new ViewResult { ViewName = "Error" }; //hata alındığında araya girecek bir yeni result oluşturduk
                result.StatusCode = 500; //errorcode 500 çünkü exception'a düşüldü
                result.ViewData = new ViewDataDictionary(_metadataProvider, context.ModelState);
                result.ViewData.Add("MvcErrorModel", mvcModelError); //Yukarıda araya giren ViewResult'ta kullanmak istenen modeli ekledik
                context.Result = result; //kullanıcıya dönecek result'a kendi oluşturduğumuzu atadık..
            }
        }
    }
}
