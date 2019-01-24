using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using RestWithAspNetCoreUdemy.Bussines;
using RestWithAspNetCoreUdemy.Bussines.Implementattions;
using RestWithAspNetCoreUdemy.Models.Context;
using RestWithAspNetCoreUdemy.Repository;
using RestWithAspNetCoreUdemy.Repository.Generic;
using RestWithAspNetCoreUdemy.Repository.Implementattions;
using RestWithAspNetCoreUdemy.Security.Configuration;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;


namespace RestWithAspNetCoreUdemy
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }
        public ILogger<Startup> Logger { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment, ILogger<Startup> logger)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
            Logger = logger;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("MySqlConnectionString");
            services.AddDbContext<MysqlContext>(context => context.UseMySql(connectionString));

            ExecuteMigrations(connectionString);

            var signingConfigurations = new SigningConfiguration();
            services.AddSingleton(signingConfigurations);

            var tokenConfiguration = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>
                (Configuration.GetSection("TokenConfigurations"))
                .Configure(tokenConfiguration);
            services.AddSingleton(tokenConfiguration);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidIssuer = tokenConfiguration.Issuer;
                paramsValidation.ValidAudience = tokenConfiguration.Audience;

                //Valida a assinatura do Token recebido
                paramsValidation.ValidateIssuerSigningKey = true;
                //Verifica se um token recebido é válido
                paramsValidation.ValidateLifetime = true;
                //Define o tempo de tolerância
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer",
                    new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepositoryImp>();
            services.AddScoped<IPersonBussines, PersonBussinesImp>();
            services.AddScoped<IBookBussines, BookBussinesImp>();
            services.AddScoped<ILoginBussines, LoginBussinesImp>();

            services.AddApiVersioning(options => options.ReportApiVersions = true);

            services.AddSwaggerGen(g =>
            {
                g.SwaggerDoc("v1", new Info
                {
                    Title = "RESTful API With ASP.NET Core 2.2",
                    Version = "v1"
                });
            });

            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("text/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            })
            .AddXmlSerializerFormatters()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1"));

            // Starting our API in Swagger page
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "DefaultApi", 
                    template: "{controller=Values}/{id?}");
            });
        }

        private void ExecuteMigrations(string connectionString)
        {
            try
            {
                if (HostingEnvironment.IsDevelopment())
                {
                    var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

                    var evolve = new Evolve.Evolve(evolveConnection, msg => Logger.LogInformation(msg))
                    {
                        Locations = new List<string> { "Db/migrations" },
                        IsEraseDisabled = true
                    };

                    evolve.Migrate();
                }
            }
            catch (Exception ex)
            {
                Logger.LogCritical("Database Migration Failed", ex);
            }
        }
    }
}
