using System;
using Core.Application.Interfaces.RedisCache;
using Core.Application.Interfaces.Tokens;
using Infrastructure.RedisCache;
using Infrastructure.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class Registration
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Jwt Token configuration for startup.
        services.Configure<TokenSettings>(configuration.GetSection("JWT"));
        services.AddTransient<ITokenService, TokenService>();

        services.AddAuthentication(opt=> {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt => {
            opt.SaveToken = true;
            var tokenSettings = configuration.GetSection("JWT").Get<TokenSettings>();
            opt.TokenValidationParameters = new TokenValidationParameters{
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = tokenSettings.Issuer,
                ValidAudience = tokenSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenSettings.Secret)),
                ClockSkew = TimeSpan.Zero
            };
        });

        // Add other services here
        services.Configure<RedisCacheSettings>(configuration.GetSection("RedisCacheSettings"));
        services.AddTransient<IRedisCacheService, RedisCacheService>();

        services.AddStackExchangeRedisCache(options => {
            var redisCacheSettings = configuration.GetSection("RedisCacheSettings").Get<RedisCacheSettings>();
            options.Configuration = redisCacheSettings.ConnectionString;
            options.InstanceName = redisCacheSettings.InstanceName;
        });

    }

}
