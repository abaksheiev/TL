using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using TL.Contracts.Models;
using TL.Contracts.Repositories;
using TL.Contracts.Services;
using TL.Repositories;
using TL.Repositories.Configurations;
using TL.Repositories.Models;
using TL.Services;
using TL.WebCore.Validators;
using MediatR;
using System.Reflection;
using TL.Services.Handlers;

namespace TL.WebInit
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            var assembly = typeof(Program).Assembly;
            services.AddMvc()
                .AddApplicationPart(assembly)
                .AddControllersAsServices();

            // Add AutoMapper with a custom mapping profile
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Register IoC
            services.AddScoped<IBookService, BookService>();
            services.AddScoped(typeof(IBookRepository<Book>), typeof(BookRepository));

            services.AddSingleton(typeof(IValidator<BookModel>), typeof(BookModelValidator));

            services.AddDbContext<CatalogContext>(options =>
                       options.UseInMemoryDatabase("catalog"));

            services.AddMediatR(i=>i.RegisterServicesFromAssembly(typeof(Startup).Assembly));
            services.AddMediatR(i => i.RegisterServicesFromAssembly(typeof(GetBookQueryHandler).Assembly));

            services.AddVersionedApiExplorer(config =>
            {
                config.GroupNameFormat = "'v'VVV";
                config.SubstituteApiVersionInUrl = true;
            });

            services.AddEndpointsApiExplorer();
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "Your API", Version = "v2" });
            });



            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => {

                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "1.0");
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "2.0");
                });
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Book}/{action=Get}/{id?}");
                });
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }
        }
    }
}
