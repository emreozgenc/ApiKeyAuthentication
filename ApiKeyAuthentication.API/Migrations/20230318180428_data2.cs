using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiKeyAuthentication.API.Migrations
{
    /// <inheritdoc />
    public partial class data2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("74baad9c-0157-46c5-918d-7c6ff1ac4acc"));

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("b1e210d3-2159-4108-b57b-2d0645d87a2f"), "test client" });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { 1, "comments.read" },
                    { 2, "comments.write" },
                    { 3, "blogs.read" },
                    { 4, "blogs.write" }
                });

            migrationBuilder.InsertData(
                table: "ApiKeys",
                columns: new[] { "Id", "ClientId", "Value" },
                values: new object[] { new Guid("9556c87b-4119-4aa1-acaf-3229a6aae8c7"), new Guid("b1e210d3-2159-4108-b57b-2d0645d87a2f"), "c/lwddFKxJt7J/S3xGBuJyNQ5VSK5js59pBkCdpZ" });

            migrationBuilder.InsertData(
                table: "ApiKeyPermissions",
                columns: new[] { "ApiKeyId", "PermissionId" },
                values: new object[,]
                {
                    { new Guid("9556c87b-4119-4aa1-acaf-3229a6aae8c7"), 1 },
                    { new Guid("9556c87b-4119-4aa1-acaf-3229a6aae8c7"), 2 },
                    { new Guid("9556c87b-4119-4aa1-acaf-3229a6aae8c7"), 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApiKeyPermissions",
                keyColumns: new[] { "ApiKeyId", "PermissionId" },
                keyValues: new object[] { new Guid("9556c87b-4119-4aa1-acaf-3229a6aae8c7"), 1 });

            migrationBuilder.DeleteData(
                table: "ApiKeyPermissions",
                keyColumns: new[] { "ApiKeyId", "PermissionId" },
                keyValues: new object[] { new Guid("9556c87b-4119-4aa1-acaf-3229a6aae8c7"), 2 });

            migrationBuilder.DeleteData(
                table: "ApiKeyPermissions",
                keyColumns: new[] { "ApiKeyId", "PermissionId" },
                keyValues: new object[] { new Guid("9556c87b-4119-4aa1-acaf-3229a6aae8c7"), 3 });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ApiKeys",
                keyColumn: "Id",
                keyValue: new Guid("9556c87b-4119-4aa1-acaf-3229a6aae8c7"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("b1e210d3-2159-4108-b57b-2d0645d87a2f"));

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("74baad9c-0157-46c5-918d-7c6ff1ac4acc"), "test client" });
        }
    }
}
