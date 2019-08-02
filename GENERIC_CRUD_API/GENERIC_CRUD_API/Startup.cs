using GENERIC_CRUD_API.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace GENERIC_CRUD_API
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
            services.AddDbContext<ReferenceContext>(opt =>
                opt.UseInMemoryDatabase("ReferenceList"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Documentação API Swagger",
                        Version = "v1",
                        Description = "Projeto de demonstração ASP.Net Core",
                        Contact = new Contact
                        {
                            Name = "Name",
                            Url = "https://localhost:44364/api"
                        }
                    });

                    var applicationBasePath = PlatformServices.Default.Application.ApplicationBasePath;
                    var applicationName = PlatformServices.Default.Application.ApplicationName;
                    var xmlDocumentPath = Path.Combine(applicationBasePath, $"{applicationName}.xml");

                    if (File.Exists(xmlDocumentPath))
                    {
                        options.IncludeXmlComments(xmlDocumentPath);
                    }
            });
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

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo JWT Api");
            });

        }
    }
}
