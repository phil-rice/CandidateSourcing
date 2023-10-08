namespace xingyi.job
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using xingyi.job.Repository;
    using Newtonsoft.Json;
    using Microsoft.AspNetCore.Mvc;
    using gui.Middleware;
    using static xingyi.job.Repository.ApplicationRepository;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                     .AddNewtonsoftJson(options =>
                     {
                         options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                         options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                     });

            services.AddDbContext<JobDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Job"), b => b.MigrationsAssembly("jobApi"))
            );

            services.AddScoped<IJobAndAppRepository, JobAndAppRepository>();
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<ISectionTemplateRepository, SectionTemplateRepository>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<IAnswersRepository, AnswersRepository>();
            services.AddScoped<ISectionRepository, SectionRepository>();



            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Jobs", Version = "v1.0" });
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
                app.UseExceptionHandler("/Home/Error");
                // Use HSTS if required
                // app.UseHsts();
            }
            app.UseMiddleware<RequestResponseLoggingMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Jobs");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // If using JWT, insert the JWT middleware here.
            // app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); 
                                           
            });
        }
    }
}
