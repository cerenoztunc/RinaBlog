using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project.BLL.Abstract;
using Project.BLL.Concrete;
using Project.DAL.Abstract;
using Project.DAL.Concrete;
using Project.DAL.Concrete.EntityFramework.Context;
using Project.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection,string connectionString)
        {
            serviceCollection.AddDbContext<RinaBlogContext>(options => options.UseSqlServer(connectionString)); //DbContext'imizi kayıt ettik..
            serviceCollection.AddIdentity<User, Role>(options => {
                //user password options
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                //user username and email options
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+$";
                options.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<RinaBlogContext>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>(); //Eğer biri senden IUnitOfWork isterse UnitOfWork demiş olduk..Buradaki scoped olarak eklememizin sebebi dbcontext'in de özünde scope olmasıdır..Scoped her request'te nesnesinin tekrar oluşmasını ve bir request içerisinde sadece bir tane nesne kullanılmasını sağlar. 
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IArticleService, ArticleManager>();

            return serviceCollection;
        }
    }
}
