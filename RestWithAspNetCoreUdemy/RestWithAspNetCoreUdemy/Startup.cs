using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestWithAspNetCoreUdemy.Bussines;
using RestWithAspNetCoreUdemy.Bussines.Implementattions;
using RestWithAspNetCoreUdemy.Models.Context;
using RestWithAspNetCoreUdemy.Repository;
using RestWithAspNetCoreUdemy.Repository.Implementattions;

namespace RestWithAspNetCoreUdemy
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
            services.AddDbContext<MysqlContext>(context => 
                context.UseMySql(Configuration.GetConnectionString("MySqlConnectionString")));

            services.AddScoped<IPersonRepository, PersonRepositoryImp>();
            services.AddScoped<IPersonBussines, PersonBussinesImp>();

            services.AddApiVersioning(options => options.ReportApiVersions = true);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
