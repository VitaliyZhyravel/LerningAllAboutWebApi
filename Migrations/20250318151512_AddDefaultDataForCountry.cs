using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LearningWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultDataForCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Faith", "Name", "NumberOfPeople" },
                values: new object[,]
                {
                    { new Guid("4cf2b79e-e2b5-4269-8b6e-c97d1bce34b8"), "Christianity", "Ukraine", 37265521 },
                    { new Guid("910b4080-4425-4e9d-a69c-d2c805f83304"), "Christianity", "United Kindome", 78562981 },
                    { new Guid("c9f9762e-ab79-4c6a-979a-7ca7f3cb6099"), "Christianity", "France", 40678564 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("4cf2b79e-e2b5-4269-8b6e-c97d1bce34b8"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("910b4080-4425-4e9d-a69c-d2c805f83304"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c9f9762e-ab79-4c6a-979a-7ca7f3cb6099"));
        }
    }
}
