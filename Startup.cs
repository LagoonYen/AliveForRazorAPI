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
                    Title = "PTCG�d�P�ӫ� API",
                    Version = "v1",
                    Description = "���ѦU���U��API�걵"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
            });

            // �`�J�������O����֨�
            services.AddDistributedMemoryCache();
            // �`�JSession
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(10);  //�i�H�]�w�ɶ�
            });

            // �`�J HttpContextAccessor
            services.AddHttpContextAccessor();

            //cookie�]�w  //�|�s����MerberInfoController
            //double LoginExpireMinute = this.Configuration.GetValue<double>("LoginExpireMinute");
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
            //{
            //    option.LoginPath = new PathString("/Home"); //�n�J��
            //    option.LogoutPath = new PathString("/Category"); //�n�XAction
            //    //�Τ᭶�����d�Ӥ[�A�n�J�O���A��Controller��Action�̥Τ�n�J�ɡA�]�i�H�]�w��
            //    option.ExpireTimeSpan = TimeSpan.FromMinutes(LoginExpireMinute);//�S���w�]14��
            //    //����w��ĳfalse�A�սc�z���n��|�n�Dcookie���ੵ�i�Ĵ��A�o�ɳ]false�ܦ�����O���ɶ�
            //    option.SlidingExpiration = false;
            //});

            //services.AddControllersWithViews(options => {
            //    //���MCSRF��w�����A�o�̴N�[�J�������ҽd��Filter���ܡA�ݷ|Controller�N�����A�[�W[AutoValidateAntiforgeryToken]�ݩ�
            //    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            //});


            // Service �� Repository�ҥ�
            services.AddScoped<MemberService, MemberServiceImpl>();
            services.AddScoped<MemberRepository, MemberRepositoryImpl>();
            services.AddScoped<ProductService, ProductServiceImpl>();
            services.AddScoped<ProductRepository, ProductRepositoryImpl>();
            services.AddScoped<ShopCarService, ShopCarServiceImpl>();
            services.AddScoped<ShopCarRepository, ShopCarRepositoryImpl>();

            // �`�J���Ҫ���
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

            app.UseHttpsRedirection(); //�o�˪��ܡAController�BAction�����A�[�W[RequireHttps]�ݩ�
            app.UseStaticFiles();

            app.UseRouting();

            // �ϥ�Session
            app.UseSession();

            //�d�N�gCode���ǡA����������...
            app.UseAuthentication();
            app.UseAuthorization(); //Controller�BAction�~��[�W [Authorize] �ݩ�

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
