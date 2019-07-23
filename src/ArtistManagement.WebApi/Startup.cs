﻿using ArtistManagement.WebApi.Bootstrap;
using ArtistManagement.WebApi.Infrastructure.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArtistManagement.WebApi
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
            services
                .AddMvc(o => {
                    o.Filters.Add(typeof(ValidateModelStateFilter));
                    o.Filters.Add(typeof(HttpGlobalExceptionFilter));
                })
                .AddControllersAsServices()
                .AddFluentValidation(o => o.RunDefaultMvcValidationAfterFluentValidationExecutes = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddArtistManagement(Configuration);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseArtistMigration(env);
            
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
