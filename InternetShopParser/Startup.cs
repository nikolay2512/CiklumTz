using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using InternetShopParser.Model.Database;
using InternetShopParser.Model.Database.Options;
using InternetShopParser.Model.Database.Services;
using InternetShopParser.Model.Services;
using InternetShopParser.View;
using InternetShopParser.View.Response;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftExtensions = Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Swagger;

namespace InternetShopParser
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors();

            services.AddOptions();
            services.AddDataProtection();

            services.Configure<ProjectOption>(Configuration.GetSection("Project"));
            services.Configure<StoreParserOption>(Configuration.GetSection("StoreParser"));

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });
#if !TEST
            var connectionString = Configuration.GetConnectionString("InternetShopContext");
#else
            var connectionString = ConnectionStringStatic.ConnectionString;
#endif
            services.AddEntityFrameworkNpgsql().AddDbContext<InternetShopDbContext>(options => options.UseNpgsql(connectionString));

            services.AddSwaggerGen(cw =>
            {
                cw.SwaggerDoc("v1", new Info
                {
                    Title = "InternetShop",
                    Version = "v1"
                });

                cw.DocumentFilter<SetVersionInPaths>();
            });
            services.AddScoped<ISourceHtmlLoaderService, SourceHtmlLoaderService>();
            services.AddScoped<IHtmlParserService, HtmlParserService>();

            services.AddTransient(typeof(SeedDatabase));
            services.AddScoped(typeof(WebClient));

            services.AddScoped<IProductService, ProductService>();

            services.AddTransient<IDateTimeProvider, SystemDateTimeProvider>();
            services.AddTransient<IStoreParserProvider, StoreParserProvider>();
            services.AddTransient<IProjectInfoProvider, ProjectInfoProvider>();
            services.AddTransient<IViewMapper, ResponseViewMapper>();
            services.AddSingleton<MicrosoftExtensions.IHostedService, TimeHostedService>((s =>
            {
                using (var scope = s.CreateScope())
                {
                    var htmlParserService = scope.ServiceProvider.GetService<IHtmlParserService>();
                    var dateTimeProvider = scope.ServiceProvider.GetService<IDateTimeProvider>();
                    return new TimeHostedService(connectionString, htmlParserService, dateTimeProvider);
                }
            }));


            Mapper.Reset();
            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddSingleton(new MapperConfiguration(x =>
            {
                x.AddProfile(new ModelToViewMapperProfile());
            }).CreateMapper());


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SeedDatabase seedDatabase)
        {
            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseCors(options =>
            {
                options.AllowAnyMethod();
                options.AllowAnyHeader();
                options.AllowAnyOrigin();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();
#if !TEST
            app.UseMvc(routes =>
                       routes.MapRoute(
                           name: "default",
                           template: "{controller=Product}/{action=ProductList}"));
#endif
            app.UseSwagger();
            app.UseSwaggerUI(cw =>
            {
                cw.DisplayRequestDuration();
                cw.InjectStylesheet("/swagger-ui/custom.css");
                cw.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");
            });

#if !TEST
            //AUTOCUT-S
            seedDatabase.Seed();
            //AUTOCUT-F    
#endif
        }
    }
}
