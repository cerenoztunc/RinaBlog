using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.BLL.AutoMapper.Profiles;
using Project.BLL.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Project.UI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews() //MVC projesi olarak çalýþmasý gerektiðini söyledik..Ayný zamanda aþaðýdaki Configure içine herhangi bir istek gekdiðinde uygulamanýn nereye gideceði verilmelidir..
                .AddRazorRuntimeCompilation() //Forntend tarafýnda her bir deðiþiklikte kodlarýmýzý yeniden derlememize gerek kalmasýn, derlemeden deðiþklikleri görebilelim diye ekledik..
                .AddJsonOptions(opt=> {
                    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                
                });
            services.AddSession();

            services.AddAutoMapper(typeof(CategoryProfile),typeof(ArticleProfile)); //Derlenme esnasýnda automapper'ýn burdaki sýnýflarý taramasýný saðlar..
            services.LoadMyServices();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Admin/User/Login"); 
                options.LogoutPath = new PathString("/Admin/User/Logout");
                options.Cookie = new CookieBuilder
                {
                    Name = "RinaBlog",
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict, //cookie bilgilerinin sadece bizim sitemiz üzerinden gelen bilgilerse kabul eder..
                    SecurePolicy = CookieSecurePolicy.SameAsRequest
                };
                options.SlidingExpiration = true; //giriþ yapan kullanýcýlara verilen zaman
                options.ExpireTimeSpan = System.TimeSpan.FromDays(7); // Kullanýcý 7 gün boyunca giriþli kalýr
                options.AccessDeniedPath = new PathString("/Admin/User/AccessDenied"); //yetkisiz giriþler
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages(); //Hatalarýn uyarýlarýný page'de görmemizi saðlar

            }
            app.UseSession();

            app.UseStaticFiles(); //Resimler, css dosyalarý, js dosyalarý kullanmamýzý saðlar.
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name:"Admin", 
                    areaName:"Admin", 
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
                    ); //Admin Area'nýn route'ýný belirledik...Örneðin biz admin area içindeki article index'e gidersek burada eklenmiþ olan tüm makaleleri bir tablo içerisinde görebileceðiz. Burda crud iþlemleri yapabiliriz. Ancak direkt olarak sitedeki article index'e gidersek buradaki tüm makalaleri bir blog tablosunda göreceðiz...
                endpoints.MapDefaultControllerRoute(); //Varsayýlan olarak site açýldýðýnda HomeController ve Index kýsmýna git dedik..
            });
        }
    }
}
