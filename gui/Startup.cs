﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using xingyi.job.Client;
using xingyi.job.Repository;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Logging;
using gui.Middleware;
using gui.Pages;
using xingyi.jobClient.admin;

namespace xingyi.gui
{
    public class Startup
    {
        //Why: This is set up like this so that I can have access to googleSetup externally. IConfiguration would be the other option... but more awkward for me
        private static string googleClientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID");
        private static string googleClientSecret = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET");


        public bool googleSetup { get; } = googleClientId != null && googleClientSecret != null;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddDistributedMemoryCache();
            services.AddSession();


            services.Configure<AdminClientSettings>(Configuration.GetSection("ApiSettings"));
            services.AddHttpClient<AdminHttpClient, AdminHttpClient>();

            services.Configure<JobAndAppSettings>(Configuration.GetSection("ApiSettings"));
            services.AddHttpClient<IJobAndAppRepository, JobAndAppClient>();

            services.Configure<JobSettings>(Configuration.GetSection("ApiSettings"));
            services.AddHttpClient<IJobRepository, JobClient>();

            services.Configure<ManagedBySettings>(Configuration.GetSection("ApiSettings"));
            services.AddHttpClient<IManagedByRepository, ManagedByClient>();

            services.Configure<SectionTemplateSettings>(Configuration.GetSection("ApiSettings"));
            services.AddHttpClient<ISectionTemplateRepository, SectionTemplateClient>();


            services.Configure<ApplicationSettings>(Configuration.GetSection("ApiSettings"));
            services.AddHttpClient<IApplicationRepository, ApplicationClient>();

            services.Configure<SectionSettings>(Configuration.GetSection("ApiSettings"));
            services.AddHttpClient<ISectionRepository, SectionClient>();

            services.AddRazorPages();
            services.AddMvc().AddViewOptions(options =>
            {
                options.HtmlHelperOptions.ClientValidationEnabled = true;
            });

            Console.WriteLine($"Setting up for google login {googleClientSecret}");

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;

            })
             .AddCookie(options => options.LoginPath = "/Identity/Login"
            )
            .AddGoogle(options =>

            {
                options.ClientId = Configuration["Authentication:Google:ClientId"];
                options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                options.CallbackPath = "/signin-google";
            });
            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", builder =>
                {
                    builder.WithOrigins(Configuration["AllowedHosts"])
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseRequestResponseLogging(options => { options.Enabled = false; });

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();
            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });

        }
    }
}
