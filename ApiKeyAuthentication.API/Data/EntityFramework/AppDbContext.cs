using ApiKeyAuthentication.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Security.Cryptography;

namespace ApiKeyAuthentication.API.Data.EntityFramework;
public class AppDbContext : DbContext
{
    private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=App;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ApiKey> ApiKeys { get; set; }
    public DbSet<ApiKeyPermission> ApiKeyPermissions { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Permission> Permissions { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApiKey>(p =>
        {
            p.HasKey(x => x.Id);

            p.HasOne(x => x.Client)
            .WithMany(x => x.ApiKeys)
            .HasForeignKey(x => x.ClientId);

            p.Property(x => x.Active)
            .HasDefaultValue(true);
        });

        modelBuilder.Entity<ApiKeyPermission>(p =>
        {
            p.HasKey(x => new { x.ApiKeyId, x.PermissionId });

            p.HasOne(x => x.Permission)
            .WithMany(x => x.ApiKeys)
            .HasForeignKey(x => x.PermissionId);

            p.HasOne(x => x.ApiKey)
            .WithMany(x => x.Permissions)
            .HasForeignKey(x => x.ApiKeyId);

        });

        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        Guid apiKeyGuid = Guid.Parse("9556c87b-4119-4aa1-acaf-3229a6aae8c7");
        Guid clientGuid = Guid.Parse("b1e210d3-2159-4108-b57b-2d0645d87a2f");
        modelBuilder.Entity<Client>(p =>
        {
            p.HasData(new[]
            {
                new Client
                {
                    Id = clientGuid,
                    Name = "test client",
                }
            });
        });

        modelBuilder.Entity<Permission>(p =>
        {
            p.HasData(new[]
            {
                new Permission
                {
                    Id = 1,
                    Value = "comments.read"
                },
                new Permission
                {
                    Id = 2,
                    Value = "comments.write"
                },
                new Permission
                {
                    Id = 3,
                    Value = "blogs.read"
                },
                new Permission
                {
                    Id = 4,
                    Value = "blogs.write"
                }
            });
        });

        modelBuilder.Entity<ApiKey>(p =>
        {
            var apiKey = RandomNumberGenerator.GetBytes(30);
            var base64 = Convert.ToBase64String(apiKey);

            p.HasData(new[]
            {
                new ApiKey
                {
                    ClientId = clientGuid,
                    Id = apiKeyGuid,
                    Value = base64
                }
            });
        });

        modelBuilder.Entity<ApiKeyPermission>(p =>
        {
            p.HasData(new[]
            {
                new ApiKeyPermission
                {
                    ApiKeyId = apiKeyGuid,
                    PermissionId = 1,
                },
                new ApiKeyPermission
                {
                    ApiKeyId = apiKeyGuid,
                    PermissionId = 2,
                },
                new ApiKeyPermission
                {
                    ApiKeyId = apiKeyGuid,
                    PermissionId = 3,
                },
            });
        });
    }
}
