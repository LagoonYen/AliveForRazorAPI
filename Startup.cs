using System;
using System.IO;
using System.Reflection;
using AliveStoreTemplate.Common;
using AliveStoreTemplate.Model;
using AliveStoreTemplate.Repositories;
using AliveStoreTemplate.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AliveStoreTemplate
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddControllers();

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PTCG卡牌商城 API",
                    Version = "v1",
                    Description = "提供各式各樣API串接"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
            });

            // 注入分散式記憶體快取
            services.AddDistributedMemoryCache();
            // 注入Session
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(10);  //可以設定時間
            });

            // 注入 HttpContextAccessor
            services.AddHttpContextAccessor();

            //cookie設定  //會叫不到MerberInfoController
            //double LoginExpireMinute = this.Configuration.GetValue<double>("LoginExpireMinute");
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
            //{
            //    option.LoginPath = new PathString("/Home"); //登入頁
            //    option.LogoutPath = new PathString("/Category"); //登出Action
            //    //用戶頁面停留太久，登入逾期，或Controller的Action裡用戶登入時，也可以設定↓
            //    option.ExpireTimeSpan = TimeSpan.FromMinutes(LoginExpireMinute);//沒給預設14天
            //    //↓資安建議false，白箱弱掃軟體會要求cookie不能延展效期，這時設false變成絕對逾期時間
            //    option.SlidingExpiration = false;
            //});

            //services.AddControllersWithViews(options => {
            //    //↓和CSRF資安有關，這裡就加入全域驗證範圍Filter的話，待會Controller就不必再加上[AutoValidateAntiforgeryToken]屬性
            //    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            //});


            // Service 及 Repository啟用
            services.AddScoped<MemberService, MemberServiceImpl>();
            services.AddScoped<MemberRepository, MemberRepositoryImpl>();
            services.AddScoped<ProductService, ProductServiceImpl>();
            services.AddScoped<ProductRepository, ProductRepositoryImpl>();
            services.AddScoped<ShopCarService, ShopCarServiceImpl>();
            services.AddScoped<ShopCarRepository, ShopCarRepositoryImpl>();

            // 注入驗證物件
            services.AddScoped(typeof(CodeValidator), typeof(CodeValidatorImpl));

            services.AddTransient<ShopContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "PTCGSHOP API");
                x.RoutePrefix = String.Empty;
            });

            app.UseHttpsRedirection(); //這樣的話，Controller、Action不必再加上[RequireHttps]屬性
            app.UseStaticFiles();

            app.UseRouting();

            // 使用Session
            app.UseSession();

            //留意寫Code順序，先執行驗證...
            app.UseAuthentication();
            app.UseAuthorization(); //Controller、Action才能加上 [Authorize] 屬性

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
