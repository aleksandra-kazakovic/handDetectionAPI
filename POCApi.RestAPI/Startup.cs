using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using POCApi.Infrastructure.Contexts;
using FluentValidation.AspNetCore;
using Newtonsoft.Json;
using POCApi.RestAPI.AcitonFilter;
using POCApi.RestAPI.JsonConverters;

namespace POCApi.RestAPI
{
    public class Startup
    {
        readonly string allowSpecificOrigins = "_allowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(config =>
            {
                config.Filters.Add(new ValidateModelActionFilterAttribute());
            }).AddJsonOptions(j =>
            {
                j.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "POCApi.RestAPI", Version = "v1" });
                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer"
                });
                c.OperationFilter<AuthenticationRequirementsOperationFilter>();
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: allowSpecificOrigins,
                 builder =>
                 {
                     builder.AllowAnyOrigin()
                    // WithOrigins(Configuration["AllowedOrigins"])
                     .AllowAnyHeader().AllowAnyMethod();
                 });
            });
            services.Configure<Microsoft.AspNetCore.Mvc.ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services
                .AddDatabase(Configuration)
                .AddRepositories()
                .AddServices()
                .AddHttpContextAccessor();

            services.AddAutoMapper(typeof(Startup));
            services.AddFluentValidation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext context, ILogger<Startup> logger)
        {
            context.Database.Migrate();
            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
            }
            app.UseExceptionHandler(new ExceptionHandlerOptions()
            {
                // AllowStatusCode404Response = true,
                ExceptionHandler = async context =>
                {
                    await ErrorHandler.HandleAsync(Configuration, context, logger);
                }
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "POCApi.RestAPI v1"));

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(allowSpecificOrigins);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
