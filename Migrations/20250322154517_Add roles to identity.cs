using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LearningWebApi.Migrations
{
    /// <inheritdoc />
    public partial class Addrolestoidentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1afeed65-8bee-46e0-b825-8fee33bb1233"), null, "Admin", "ADMIN" },
                    { new Guid("1afeed65-8bee-46e0-b825-8fee33bb3200"), null, "Admin", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1afeed65-8bee-46e0-b825-8fee33bb1233"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1afeed65-8bee-46e0-b825-8fee33bb3200"));
        }
    }
}
