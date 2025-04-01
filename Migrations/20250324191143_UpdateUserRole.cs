using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningWebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1afeed65-8bee-46e0-b825-8fee33bb3200"),
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "User", "USER" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1afeed65-8bee-46e0-b825-8fee33bb3200"),
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Admin", "User" });
        }
    }
}
