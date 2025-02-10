using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TaskBoard.Framework.Core.Security.Authentication;
using TaskBoard.Framework.Core.Utils.Swagger;
using TaskBoard.Service.Core.Domain.Mappers;
using TaskBoard.Service.Core.Domain.Repositories;
using TaskBoard.Service.Core.Domain.Services;
using TaskBoard.Service.Core.Infrastructure.Repositories;

namespace TaskBoard.Service.Core.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        #region Mappers
        services.AddTransient<ITodoMapper, TodoMapper>();
        #endregion

        #region Services
        services.AddScoped<ITodoService, TodoService>();
        services.AddScoped<IUserService, KeycloakUserService>();
        #endregion

        #region Repositories
        services.AddScoped<ITodoRepository, TodoRepository>();
        #endregion

        #region Authentication
        services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
        #endregion

        return services;
    }

    public static IServiceCollection ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        return services;
    }

    public static IServiceCollection ConfigureSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.OperationFilter<CustomResponseContentTypeFilter>();

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "TaskBoard.Service.Core API",
                Version = "0.0.1",
                Description = "Personal Tasks management Web API",
            });

            var securityScheme = new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme.Example: \"Authorization: Bearer {token}\"",

                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            options.AddSecurityDefinition("Bearer", securityScheme);

            options.AddSecurityRequirement(new OpenApiSecurityRequirement 
            {
                {
                    securityScheme,
                    new[] { "Bearer" }
                }
            });
        });

        return services;
    }
}
