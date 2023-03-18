using ApiKeyAuthentication.API.Authorization.ApiKey;
using ApiKeyAuthentication.API.Data.Entities;
using ApiKeyAuthentication.API.Data.EntityFramework;
using ApiKeyAuthentication.API.Data.Repositories.Abstract;
using ApiKeyAuthentication.API.Data.Repositories.Concrete;
using ApiKeyAuthentication.API.Services.Abstract;
using ApiKeyAuthentication.API.Services.Concrete;
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
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddAuthentication()
    .AddApiKey(options => { });

builder.Services.AddAuthorization(configure =>
{
    //configure.AddPolicy("DefaultPolicy", policy =>
    //{
    //    policy.RequireAuthenticatedUser()
    //    .RequireClaim("permission", "blogs.read")
    //    .AddAuthenticationSchemes(ApiKeyAuthenticationOptions.DefaultSchema)
    //    .Build();
    //});
});

builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition(ApiKeyAuthenticationOptions.DefaultSchema, new OpenApiSecurityScheme
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
                    Id = ApiKeyAuthenticationOptions.DefaultSchema
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