using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Project.BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
namespace Project.UI.Attributes
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
    public class ViewCountFilterAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var articleID = context.ActionArguments["articleID"];
            if(articleID != null)
            {
                string articleValue = context.HttpContext.Request.Cookies[$"article{articleID}"]; //makale değerini elimize aldık..bu makalenin daha önce kullanıcı tarafından açılıp açılmadığını gösterecek..Eğer okunmadıysa bu değer null geleceğinden aşağıdaki kontrole düşecek ve yeni bir cookie oluşturulacak..
                if (string.IsNullOrEmpty(articleValue))
                {
                    Set($"article{articleID}", articleID.ToString(), 1, context.HttpContext.Response); //tarayıcı üzerine cookie yazdık...Aynı zamanda artık makale okunma sayısının artırılması lazım. Çünkü kullanıcı makaleyi ilk defa açtı..
                    var articleService = context.HttpContext.RequestServices.GetService<IArticleService>(); //okunma sayısını elde etmek için IArticleService' eriştik.. bu işlem kızmasın diye using olarak DependencyInjection'ı ekledik..
                    await articleService.IncreaseViewCountAsync(Convert.ToInt32(articleID)); //IArticleService'te imzalanmış metodu çağırarak makale sayısının bir artırılmasını sağladık..
                    await next(); //view'ın yüklenmesi ve işlemlerin devam etmesi için..
                }
            }
            await next();
        }
        /// <summary>  
        /// set the cookie  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">value to store in cookie object</param>  
        /// <param name="expireTime">expiration time</param>  
        public void Set(string key, string value, int? expireTime, HttpResponse response)
        {
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddYears(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMonths(6);

            response.Cookies.Append(key, value, option);
        }

        /// <summary>  
        /// Delete the key  
        /// </summary>  
        /// <param name="key">Key</param>  
        public void Remove(string key, HttpResponse response)
        {
            response.Cookies.Delete(key);
        }
    }
}
