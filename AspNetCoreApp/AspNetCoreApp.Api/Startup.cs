using AspNetCoreApp.Api.Domain;
using AspNetCoreApp.Api.Dto;
using AspNetCoreApp.Api.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Swashbuckle.AspNetCore.Swagger;
using Tag = AspNetCoreApp.Api.Domain.Tag;

namespace AspNetCoreApp.Api
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
            services.AddDbContext<TodoContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper();
            services.AddMvcCore().AddJsonFormatters();
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
                    Contact = new Contact { Name = "Atilla Yavuz", Email = "atillayavuz@gmail.com"}
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Initialize(app.ApplicationServices);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            Mapper.Initialize(mapper =>
            {
                mapper.CreateMap<Task, TaskDto>().ForMember(x => x.CreateDate,
                    opt => opt.MapFrom(src => src.CreateDate.ToShortDateString()));
                mapper.CreateMap<Tag, TagDto>().ForMember(x => x.CreateDate,
                    opt => opt.MapFrom(src => src.CreateDate.ToShortDateString()));
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AspNetCoreApp API V1");
            });
            app.UseCors("AllowAllOrigins");
            app.UseMvc();
        }
        public static void Initialize(IServiceProvider service)
        {
            using (var serviceScope = service.CreateScope())
            {
                var scopeServiceProvider = serviceScope.ServiceProvider;
                var db = scopeServiceProvider.GetRequiredService<TodoContext>();
                db.Database.Migrate();
            }
        }
    }
}
