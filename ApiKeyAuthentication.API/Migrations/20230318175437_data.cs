using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiKeyAuthentication.API.Migrations
{
    /// <inheritdoc />
    public partial class data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApiKey_Client_ClientId",
                table: "ApiKey");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiKeyPermission_ApiKey_ApiKeyId",
                table: "ApiKeyPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiKeyPermission_Permission_PermissionId",
                table: "ApiKeyPermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permission",
                table: "Permission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Client",
                table: "Client");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApiKeyPermission",
                table: "ApiKeyPermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApiKey",
                table: "ApiKey");

            migrationBuilder.RenameTable(
                name: "Permission",
                newName: "Permissions");

            migrationBuilder.RenameTable(
                name: "Client",
                newName: "Clients");

            migrationBuilder.RenameTable(
                name: "ApiKeyPermission",
                newName: "ApiKeyPermissions");

            migrationBuilder.RenameTable(
                name: "ApiKey",
                newName: "ApiKeys");

            migrationBuilder.RenameIndex(
                name: "IX_ApiKeyPermission_PermissionId",
                table: "ApiKeyPermissions",
                newName: "IX_ApiKeyPermissions_PermissionId");

            migrationBuilder.RenameIndex(
                name: "IX_ApiKey_ClientId",
                table: "ApiKeys",
                newName: "IX_ApiKeys_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApiKeyPermissions",
                table: "ApiKeyPermissions",
                columns: new[] { "ApiKeyId", "PermissionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApiKeys",
                table: "ApiKeys",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("74baad9c-0157-46c5-918d-7c6ff1ac4acc"), "test client" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApiKeyPermissions_ApiKeys_ApiKeyId",
                table: "ApiKeyPermissions",
                column: "ApiKeyId",
                principalTable: "ApiKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiKeyPermissions_Permissions_PermissionId",
                table: "ApiKeyPermissions",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiKeys_Clients_ClientId",
                table: "ApiKeys",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApiKeyPermissions_ApiKeys_ApiKeyId",
                table: "ApiKeyPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiKeyPermissions_Permissions_PermissionId",
                table: "ApiKeyPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiKeys_Clients_ClientId",
                table: "ApiKeys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApiKeys",
                table: "ApiKeys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApiKeyPermissions",
                table: "ApiKeyPermissions");

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("74baad9c-0157-46c5-918d-7c6ff1ac4acc"));

            migrationBuilder.RenameTable(
                name: "Permissions",
                newName: "Permission");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "Client");

            migrationBuilder.RenameTable(
                name: "ApiKeys",
                newName: "ApiKey");

            migrationBuilder.RenameTable(
                name: "ApiKeyPermissions",
                newName: "ApiKeyPermission");

            migrationBuilder.RenameIndex(
                name: "IX_ApiKeys_ClientId",
                table: "ApiKey",
                newName: "IX_ApiKey_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_ApiKeyPermissions_PermissionId",
                table: "ApiKeyPermission",
                newName: "IX_ApiKeyPermission_PermissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permission",
                table: "Permission",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Client",
                table: "Client",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApiKey",
                table: "ApiKey",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApiKeyPermission",
                table: "ApiKeyPermission",
                columns: new[] { "ApiKeyId", "PermissionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApiKey_Client_ClientId",
                table: "ApiKey",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiKeyPermission_ApiKey_ApiKeyId",
                table: "ApiKeyPermission",
                column: "ApiKeyId",
                principalTable: "ApiKey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiKeyPermission_Permission_PermissionId",
                table: "ApiKeyPermission",
                column: "PermissionId",
                principalTable: "Permission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
