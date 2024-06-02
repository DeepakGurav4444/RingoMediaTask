using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace RingoMediaTask.Migrations
{
    /// <inheritdoc />
    public partial class Addedremindermodule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reminders",
                columns: table => new
                {
                    IdReminder = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ReminderFor = table.Column<string>(type: "longtext", nullable: false),
                    ReminderDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    OperationalId = table.Column<int>(type: "int", nullable: false),
                    IsProcessing = table.Column<byte>(type: "tinyint unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminders", x => x.IdReminder);
                    table.ForeignKey(
                        name: "FK_Reminders_Operationals_OperationalId",
                        column: x => x.OperationalId,
                        principalTable: "Operationals",
                        principalColumn: "IdOperational",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_OperationalId",
                table: "Reminders",
                column: "OperationalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reminders");
        }
    }
}
