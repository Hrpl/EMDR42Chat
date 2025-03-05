using EMDR42Chat.Domain.Commons.DTO;
using EMDR42Chat.Domain.Commons.Singleton;
using EMDR42Chat.Domain.Models;
using EMDR42Chat.Infrastructure.Services.Implementations;
using EMDR42Chat.Infrastructure.Services.Interfaces;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace EMDR42.API.Extensions;

public static class AddServiceExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddMapster();
        services.AddRegisterService();
        services.AddOpenAPI();
    }
    public static void AddOpenAPI(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.AddServer(new OpenApiServer { Url = "/api/at" });
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Repositories", Version = "v2024" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Authorization using jwt token. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] { }
        }
    });
            });
    }

    public static void AddMapster(this IServiceCollection services)
    {
        TypeAdapterConfig config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        Mapper mapperConf = new(config);
        services.AddSingleton<IMapper>(mapperConf);
    }
    public static void AddRegisterService(this IServiceCollection services)
    {
        services.AddSingleton<Config>();
        services.AddScoped<IDbConnectionManager, DbConnectionManager>();
        services.AddScoped<IClientConnectionService, ClientConnectionService>();
    }
}
