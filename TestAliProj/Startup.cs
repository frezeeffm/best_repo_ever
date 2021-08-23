using System;
using System.Linq;
using System.Reflection;
using DAL.Models.Mappings;
using Dapper.FluentMap;
using FluentMigrator.Runner;
using Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using TestAliProj.Extensions;

namespace TestAliProj
{
    public class Startup
    {
        private IConfigurationRoot Configuration { get; }
        
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddControllers();
            
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new CategoryMapping());
            });
            
            services.AddLogging(c => c.AddFluentMigratorConsole())
                .AddFluentMigratorCore()
                .ConfigureRunner(c => c
                    .AddPostgres()
                    .WithGlobalConnectionString(Configuration.GetConnectionString("PostgresConnection"))
                    .ScanIn(AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.FullName != null && x.FullName.Contains("DAL"))).For.Migrations());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            app.Migrate();
        }
    }
}