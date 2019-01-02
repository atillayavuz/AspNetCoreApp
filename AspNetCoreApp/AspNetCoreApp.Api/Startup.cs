using AspNetCoreApp.Api.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System; 

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

            services.AddMvcCore().AddJsonFormatters();

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Initialize(app.ApplicationServices);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
