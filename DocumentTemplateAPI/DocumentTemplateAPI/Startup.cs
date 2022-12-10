using DocumentTemplateModel.Models;
using DocumentTemplateRepository.Implementations;
using DocumentTemplateRepository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentTemplateAPI
{
    public class Startup
    {
        //Scaffold-DbContext "Server=tuleap.vanlanguni.edu.vn,18082;User Id=CP25Team08;Password=CP25Team08;Database=CP25Team08;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CP25Team08Context>();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IDBRepositoryBase<User>), typeof(DBRepositoryBase<User>));
            services.AddScoped(typeof(IDBRepositoryBase<Template>), typeof(DBRepositoryBase<Template>));
            services.AddScoped(typeof(IDBRepositoryBase<Department>), typeof(DBRepositoryBase<Department>));
            services.AddScoped(typeof(IDBRepositoryBase<Role>), typeof(DBRepositoryBase<Role>));
            services.AddScoped(typeof(IDBRepositoryBase<UserRole>), typeof(DBRepositoryBase<UserRole>));
            services.AddScoped(typeof(IDBRepositoryBase<UserTemplate>), typeof(DBRepositoryBase<UserTemplate>));
            services.AddScoped(typeof(IDBRepositoryBase<Category>), typeof(DBRepositoryBase<Category>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITemplateRepository, TemplateRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserTemplateRepository, UserTemplateRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddControllers();
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "V1",
                    Title = "DocumentTemplate API",
                    Description = "API for DocumentTemplate API"
                });
            });

            services.AddCors(option =>
            {
                option.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DocumentTemplate API");
                c.RoutePrefix = String.Empty;
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Users}/{action=login}/{id?}");
            });
        }
    }
}
