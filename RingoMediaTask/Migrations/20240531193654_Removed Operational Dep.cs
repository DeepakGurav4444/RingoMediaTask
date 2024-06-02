using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RingoMediaTask.Migrations
{
    /// <inheritdoc />
    public partial class RemovedOperationalDep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_Operationals_OperationalId",
                table: "Reminders");

            migrationBuilder.DropIndex(
                name: "IX_Reminders_OperationalId",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "OperationalId",
                table: "Reminders");

            migrationBuilder.AddColumn<string>(
                name: "EmailForReminder",
                table: "Reminders",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailForReminder",
                table: "Reminders");

            migrationBuilder.AddColumn<int>(
                name: "OperationalId",
                table: "Reminders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_OperationalId",
                table: "Reminders",
                column: "OperationalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_Operationals_OperationalId",
                table: "Reminders",
                column: "OperationalId",
                principalTable: "Operationals",
                principalColumn: "IdOperational",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
