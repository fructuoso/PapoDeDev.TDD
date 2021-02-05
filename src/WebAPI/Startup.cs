using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PapoDeDev.TDD.Domain.Core.Interfaces.Repository;
using PapoDeDev.TDD.Domain.Core.Interfaces.Service;
using PapoDeDev.TDD.Domain.Services;
using PapoDeDev.TDD.Infra.Repository;
using PapoDeDev.TDD.WebAPI.Developer;
using PapoDeDev.TDD.WebAPI.Filters;
using System;
using Entity = PapoDeDev.TDD.Domain.Core.Entity;

namespace PapoDeDev.TDD.WebAPI
{
    public class Startup
    {
        public Startup() { }


        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PapoDeDev.TDD", Version = "v1" });
            });
            #endregion

            #region DbContext
            RegisterDbContext(services);
            #endregion

            #region Dependency Injectionn
            services.AddTransient(typeof(IServiceCrud<Guid, Entity.Developer>), typeof(DeveloperService));
            services.AddTransient(typeof(IDeveloperService), typeof(DeveloperService));
            services.AddTransient(typeof(IRepositoryCrud<,>), typeof(GenericRepositoryCrud<,>));
            #endregion

            #region AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            #endregion

            #region Repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #region Filters
            services.AddTransient<ModelStateFilter>();
            #endregion

            services.AddControllers(options => options.Filters.AddService<ModelStateFilter>()).AddFluentValidation();

            services.AddTransient<IValidator<DeveloperModel>, DeveloperValidator>();
        }

        protected virtual void RegisterDbContext(IServiceCollection services)
        {
            services.AddDbContext<RepositoryContext>(options => options.UseInMemoryDatabase(databaseName: "PapoDeDev.TDD_DB"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Swagger
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
            });
            #endregion

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
