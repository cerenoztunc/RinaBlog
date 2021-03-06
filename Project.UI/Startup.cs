using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.BLL.AutoMapper.Profiles;
using Project.BLL.Extensions;
using Project.ENTITIES.Concrete;
using Project.SHARED.Utilities.Extensions;
using Project.UI.AutoMapper.Profiles;
using Project.UI.Filters;
using Project.UI.Helpers.Abstract;
using Project.UI.Helpers.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Project.UI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IImageHelper, ImageHelper>();
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
              {
                  cfg.AddProfile(new UserProfile(provider.GetService<IImageHelper>()));
                  cfg.AddProfile(new CategoryProfile());
                  cfg.AddProfile(new ArticleProfile());
                  cfg.AddProfile(new ViewModelsProfile());
                  cfg.AddProfile(new CommentProfile());
              }).CreateMapper()); //Derlenme esnas?nda automapper'?n burdaki s?n?flar? taramas?n? sa?lar..Ayn? zamanda UserProfile'a nas?l taranmas? gerekti?ini s?yledik
            services.Configure<AboutUsPageInfo>(Configuration.GetSection("AboutUsPageInfo"));
            services.Configure<WebSiteInfo>(Configuration.GetSection("WebSiteInfo"));
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.Configure<ArticleRightSideBarWidgetOptions>(Configuration.GetSection("ArticleRightSideBarWidgetOptions"));
            services.ConfigureWritable<AboutUsPageInfo>(Configuration.GetSection("AboutUsPageInfo"));
            services.ConfigureWritable<WebSiteInfo>(Configuration.GetSection("WebSiteInfo"));
            services.ConfigureWritable<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.ConfigureWritable<ArticleRightSideBarWidgetOptions>(Configuration.GetSection("ArticleRightSideBarWidgetOptions"));

            services.AddControllersWithViews(options=> 
            {
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(value => "Bu alan bo? ge?ilemez!");
                options.Filters.Add<MvcExceptionFilter>();
            
            
            }) //MVC projesi olarak ?al??mas? gerekti?ini s?yledik..Ayn? zamanda a?a??daki Configure i?ine herhangi bir istek gekdi?inde uygulaman?n nereye gidece?i verilmelidir..
                .AddRazorRuntimeCompilation() //Forntend taraf?nda her bir de?i?iklikte kodlar?m?z? yeniden derlememize gerek kalmas?n, derlemeden de?i?klikleri g?rebilelim diye ekledik..
                .AddJsonOptions(opt=> {
                    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                
                }).AddNToastNotifyToastr();
            services.AddSession();

            services.LoadMyServices(connectionString:Configuration.GetConnectionString("LocalDB"));
            
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Admin/Auth/Login"); 
                options.LogoutPath = new PathString("/Admin/Auth/Logout");
                options.Cookie = new CookieBuilder
                {
                    Name = "RinaBlog",
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict, //cookie bilgilerinin sadece bizim sitemiz ?zerinden gelen bilgilerse kabul eder..
                    SecurePolicy = CookieSecurePolicy.SameAsRequest
                };
                options.SlidingExpiration = true; //giri? yapan kullan?c?lara verilen zaman
                options.ExpireTimeSpan = System.TimeSpan.FromDays(7); // Kullan?c? 7 g?n boyunca giri?li kal?r
                options.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied"); //yetkisiz giri?ler
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages(); //Hatalar?n uyar?lar?n? page'de g?rmemizi sa?lar

            }
            app.UseSession();

            app.UseStaticFiles(); //Resimler, css dosyalar?, js dosyalar? kullanmam?z? sa?lar.
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseNToastNotify();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name:"Admin", 
                    areaName:"Admin", 
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
                    ); //Admin Area'n?n route'?n? belirledik...?rne?in biz admin area i?indeki article index'e gidersek burada eklenmi? olan t?m makaleleri bir tablo i?erisinde g?rebilece?iz. Burda crud i?lemleri yapabiliriz. Ancak direkt olarak sitedeki article index'e gidersek buradaki t?m makalaleri bir blog tablosunda g?rece?iz...
                endpoints.MapControllerRoute(
                    name: "article",
                    pattern:"{title}/{articleID}",
                    defaults:new {controller="Article",action="Detail"}
                    );
                endpoints.MapDefaultControllerRoute(); //Varsay?lan olarak site a??ld???nda HomeController ve Index k?sm?na git dedik..
            });
        }
    }
}
