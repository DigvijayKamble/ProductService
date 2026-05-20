using System.IdentityModel.Tokens.Jwt;                     
using Asp.Versioning;  
using FluentValidation;
using FluentValidation.AspNetCore; 
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models; 
using ProductService.API.Middlewares;
using ProductService.Application.Interfaces;
using ProductService.Application.Services;
using ProductService.Application.Validators;
using ProductService.Infrastructure.Configuration;
using ProductService.Infrastructure.DbContexts;
using ProductService.Infrastructure.Messaging;
using ProductService.Infrastructure.Outbox;
using ProductService.Infrastructure.Repositories;

public static class Startup
{
    // =========================================================
    // Service & Dependency Configuration
    // =========================================================
    public static void ConfigureDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // -------------------- App Settings --------------------
        services.Configure<AppSettings>(configuration);

        // -------------------- Database --------------------
        var connectionString =
            configuration.GetConnectionString("ProductDB")
            ?? configuration[Constants.DatabaseConnectionString]
            ?? throw new InvalidOperationException("Database connection string not found.");

        services.AddDbContext<ProductDbContext>(options =>
        {
            options.UseSqlServer(connectionString, sql =>
            {
                sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                sql.CommandTimeout(60);
            });

            if (configuration.GetValue<bool>(Constants.EnableDevMode))
            {
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            }
        });

        // -------------------- CORS --------------------
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAngular", policy =>
            {
                policy.WithOrigins("http://localhost:4200")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });

        // -------------------- Repositories --------------------
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IMeasurementUnitRepository, MeasurementUnitRepository>();
        services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
        services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
        services.AddScoped<ITenantRepository, TenantRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        // -------------------- Business Services --------------------
        services.AddScoped<ICategoryBusinessService, CategoryBusinessService>();
        services.AddScoped<IBrandBusinessService, BrandBusinessService>();
        services.AddScoped<IProductService, ProductServices>();
        services.AddScoped<IMeasurementUnitService, MeasurementUnitService>(); 
        // -------------------- Messaging / Outbox --------------------
        services.AddHostedService<OutboxProcessor>();
        services.AddSingleton<IEventPublisher, KafkaPublisher>();

        // -------------------- Fluent Validation --------------------
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<CreateCategoryValidator>();

        // -------------------- Rate Limiting --------------------
        services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter("fixed", opt =>
            {
                opt.Window = TimeSpan.FromMinutes(1);
                opt.PermitLimit = 100;
                opt.QueueLimit = 10;
            });
        });

        // -------------------- Authentication --------------------
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://authservice/api";
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        // -------------------- API Versioning --------------------
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;

            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new QueryStringApiVersionReader("api-version"),
                new HeaderApiVersionReader("X-Api-Version")
            );
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        // -------------------- Controllers --------------------
        services.AddControllers();

        // -------------------- Swagger --------------------
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Product API",
                Version = "v1"
            });

            options.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter: Bearer {token}"
                });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        services.AddAuthorization();
    }

    // =========================================================
    // HTTP Middleware Pipeline (ORDER MATTERS)
    // =========================================================
    public static void ConfigureMiddleware(
        this WebApplication app,
        IConfiguration configuration)
    {
        // Catches all unhandled exceptions and returns consistent error responses
        app.UseMiddleware<ExceptionMiddleware>();

        // Enables Swagger only in Development
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductService API v1");
                c.RoutePrefix = "swagger";
            });
        }

        // Redirects HTTP requests to HTTPS
        app.UseHttpsRedirection();

        // Enables endpoint routing
        app.UseRouting();

        // Applies rate limiting rules
        app.UseRateLimiter();

        // Applies CORS policy
        app.UseCors("NextJsPolicy");

        // Authenticates the request and sets HttpContext.User
        app.UseAuthentication();

        // Authorizes the request based on roles/policies
        app.UseAuthorization();

        // Maps controller endpoints (must be LAST)
        app.MapControllers();
    }
}