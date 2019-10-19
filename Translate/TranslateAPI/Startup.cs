﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using TranslateAPI.Domain;

namespace TranslateAPI
{
    public class Startup
    {
        // for CORS
        private string allowedOrigin;

        private readonly string MyAllowOrigins = "_myAllowOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            allowedOrigin = Configuration["AllowedOrigins"];

            services.AddDbContext<TranslateDb>(options =>
                options.UseSqlServer(@"Data Source=GNOME;Initial Catalog = Translate; Persist Security Info = True;
                                       User ID = sa;
                                       Password = '12345';"));

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowOrigins,
                builder =>
                {
                    builder.WithOrigins(allowedOrigin);
                });
            });

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

            app.UseCors(MyAllowOrigins);
            
            app.UseHttpsRedirection();
            app.UseMvc();
            
        }
    }
}