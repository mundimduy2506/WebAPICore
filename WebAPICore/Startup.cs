using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebAPICore.Models;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebAPICore.Swagger;
using Microsoft.AspNetCore.Mvc;

namespace WebAPICore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                //.SetBasePath(env.ContentRootPath)
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            //services.AddApiVersioning();
            //Add DI for EF
            var connection = @"Data Source=LT-00005495\SQLEXPRESS;Initial Catalog=WebAPI;Persist Security Info=True;User ID=sa;Password=Abcde12345-;";
            var connection2 = @"Data Source=Seimdbdevdb2014;Initial Catalog=NETIKIP;User ID=netikapp_user;Password=netik;";
            services.AddDbContext<WebAPIContext>(options => options.UseSqlServer(connection2));
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IPortfolioRepository, PortfolioRepository>();


            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                c.SwaggerDoc("v2", new Info { Title = "My API", Version = "v2" });
                //c.DocInclusionPredicate((docName, apiDesc) =>
                //{
                    //var versions = apiDesc.ControllerAttributes()
                    //    .OfType<ApiVersionAttribute>()
                    //    .SelectMany(attr => attr.Versions);

                    //return versions.Any(v => $"v{v.ToString()}" == docName);
                //});

                c.OperationFilter<RemoveVersionParameters>();
                c.DocumentFilter<SetVersionInPaths>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");
            });
            app.UseMvc();
        }
    }
}
