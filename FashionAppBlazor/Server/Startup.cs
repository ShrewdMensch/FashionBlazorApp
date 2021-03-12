using Application;
using Application.DTOs;
using Application.PhotoAccessor;
using Application.Repository;
using Domain;
using FluentValidation;
using Infrastructure.PhotoAccessor;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;
using Persistence.Repository;
using System;

namespace FashionAppBlazor.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            if (WebHostEnvironment.EnvironmentName == "Development")
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseLazyLoadingProxies();
                    options.UseSqlite(
                        Configuration.GetConnectionString("DevelopmentDbConnection"), x => x.MigrationsAssembly("SQLiteMigrations"));
                });
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseLazyLoadingProxies();
                    options.UseMySql(
                        Configuration.GetConnectionString("ProductionDbConnection"),
                        ServerVersion.AutoDetect(Configuration.GetConnectionString("ProductionDbConnection")),
                        x => x.MigrationsAssembly("MySqlMigrations"));
                });
            }

            StartUpConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }

        private void StartUpConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppUserRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager<SignInManager<AppUser>>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 1;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.Cookie.Name = "ApplicationCookie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/Account/Login";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddAutoMapper(typeof(IRepository));
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IPhotoAccessor, PhotoAccessor>();
            services.AddScoped<IFileUpload, FileUpload>();

            services.AddValidatorsFromAssemblyContaining<AccessoryDto>();

        }
    }
}
