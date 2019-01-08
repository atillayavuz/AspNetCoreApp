using AspNetCoreApp.Api.Application.Validation;
using AspNetCoreApp.Api.Infrastructure;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Linq;

namespace AspNetCoreApp.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        { 
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                //.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                //.MinimumLevel.Override("System", LogEventLevel.Warning) 
                .Enrich.WithProperty("Application", "AspNetCoreApp_v1")
                .MinimumLevel.Debug()
                .WriteTo.RollingFile(Path.Combine(env.ContentRootPath + "/logs", "log-{Date}.txt"), outputTemplate: "{Timestamp:HH:mm:ss} [{Level}] {Application}: {Message}{NewLine}{Exception}") 
                .CreateLogger();


            //var file = File.CreateText(env.ContentRootPath + "/logs/serilog_self_.txt");
            //Serilog.Debugging.SelfLog.Enable(msg => TextWriter.Synchronized(file));

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TodoContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "AspNetCoreApp Project",
                    Description = "AspNetCoreApp API Swagger surface",
                    Contact = new Contact { Name = "Atilla Yavuz", Email = "atillayavuz@gmail.com" }
                });
            });
              
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddFluentValidation();

            services.AddApplicationValidators();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(p => p.ErrorMessage)).ToList();
                    var result = new
                    {
                        Message = "Validation errors",
                        Errors = errors
                    };
                    return new BadRequestObjectResult(result);
                };
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AspNetCoreApp API V1");
            });
            app.UseCors("AllowAllOrigins");
            app.UseMvc();
        }
    }
}
