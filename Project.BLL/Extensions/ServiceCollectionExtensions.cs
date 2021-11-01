using Microsoft.Extensions.DependencyInjection;
using Project.BLL.Abstract;
using Project.BLL.Concrete;
using Project.DAL.Abstract;
using Project.DAL.Concrete;
using Project.DAL.Concrete.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<RinaBlogContext>(); //DbContext'imizi kayıt ettik..
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>(); //Eğer biri senden IUnitOfWork isterse UnitOfWork demiş olduk..Buradaki scoped olarak eklememizin sebebi dbcontext'in de özünde scope olmasıdır..Scoped her request'te nesnesinin tekrar oluşmasını ve bir request içerisinde sadece bir tane nesne kullanılmasını sağlar. 
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IArticleService, ArticleManager>();

            return serviceCollection;
        }
    }
}
