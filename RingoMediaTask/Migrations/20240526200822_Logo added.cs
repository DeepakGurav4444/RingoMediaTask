using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RingoMediaTask.Migrations
{
    /// <inheritdoc />
    public partial class Logoadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Logo",
                table: "Departments",
                type: "longblob",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Departments");
        }
    }
}
