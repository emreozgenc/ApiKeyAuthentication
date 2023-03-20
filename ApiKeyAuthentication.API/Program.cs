using ApiKeyAuthentication.API.Authentication.ApiKey;
using ApiKeyAuthentication.API.Cache.Abstract;
using ApiKeyAuthentication.API.Cache.Concrete;
using ApiKeyAuthentication.API.Data.EntityFramework;
using ApiKeyAuthentication.API.Data.Repositories.Abstract;
using ApiKeyAuthentication.API.Data.Repositories.Concrete;
using ApiKeyAuthentication.API.Services.Abstract;
using ApiKeyAuthentication.API.Services.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IClientService, ClientManager>();
builder.Services.AddScoped<IApiKeyRepository, ApiKeyRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<ICacheService, CacheManager>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddAuthentication(ApiKeyAuthenticationOptions.DefaultScheme + "Alternative")
    .AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(ApiKeyAuthenticationOptions.DefaultScheme, _ => { })
    .AddScheme<ApiKeyAuthenticationOptions, ApiKeyAlternativeAuthenticationHandler>(ApiKeyAuthenticationOptions.DefaultScheme + "Alternative", options =>
    {
        options.ForwardDefaultSelector = context =>
        {
            if(!context.Request.Headers.TryGetValue(ApiKeyAuthenticationOptions.HeaderName + "-alternative", out var apiKey))
                return ApiKeyAuthenticationOptions.DefaultScheme;

            return null;
        };
    });

builder.Services.AddMemoryCache();
builder.Services.AddAuthorization(configure =>
{
    configure.DefaultPolicy = new AuthorizationPolicyBuilder()
                                .AddAuthenticationSchemes(ApiKeyAuthenticationOptions.DefaultScheme,
                                ApiKeyAuthenticationOptions.DefaultScheme + "Alternative")
                                .RequireAuthenticatedUser()
                                .Build();
});

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition(ApiKeyAuthenticationOptions.DefaultScheme, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = ApiKeyAuthenticationOptions.HeaderName,
        Type = SecuritySchemeType.ApiKey,
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = ApiKeyAuthenticationOptions.DefaultScheme
                }
            },
            Array.Empty<string>()
        }
    });

    c.AddSecurityDefinition(ApiKeyAuthenticationOptions.DefaultScheme + "Alternative", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = ApiKeyAuthenticationOptions.HeaderName + "-alternative",
        Type = SecuritySchemeType.ApiKey,
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = ApiKeyAuthenticationOptions.DefaultScheme + "Alternative"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
