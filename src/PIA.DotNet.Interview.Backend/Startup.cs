using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PIA.DotNet.Interview.Backend.Service;
using PIA.DotNet.Interview.Core.Database;
using PIA.DotNet.Interview.Core.Repositories;
using System;

namespace PIA.DotNet.Interview.Backend
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // core
            services.AddSingleton<IDbContext, DbContext>();
            services.AddSingleton<ITaskRepository, TaskRepository>();

            // service
            services.AddSingleton<ITaskLogicService, TaskLogicService>();
            services.AddSingleton<IMeasurementService, MeasurementService>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "pia API",
                    Version = "v1",
                    Description = "Description for the API goes here.",
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
