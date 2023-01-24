using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using WebApi.Handler;

namespace WebApi.Extensions;

public static class AuthServiceExtension
{
    public static IServiceCollection AddAuthServices(this IServiceCollection services, string domain, string audiance)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = domain;
            options.Audience = audiance;

        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireClaim("permissions", "Ldv:admin")); 
            options.AddPolicy("Client", policy => policy.RequireClaim("permissions", "Ldv:client")); 
        });
        services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
        return services;
    }
}
