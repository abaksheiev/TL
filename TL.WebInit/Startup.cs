using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TL.Contracts.Models;
using TL.Contracts.Repositories;
using TL.Contracts.Services;
using TL.Repositories;
using TL.Repositories.Configurations;
using TL.Repositories.Models;
using TL.Services;
using TL.WebCore.Validators;
using TL.WebInit.Controllers;

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
            services.AddControllers();
            // Add services to the container.
            var assembly = typeof(BookController).Assembly;
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // Enable Swagger UI only in Development environment
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1"));
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
