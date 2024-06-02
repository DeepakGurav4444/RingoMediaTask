using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RingoMediaTask.Migrations
{
    /// <inheritdoc />
    public partial class removedisactivefrombothtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Operationals");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Managements");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Operationals",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Managements",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
