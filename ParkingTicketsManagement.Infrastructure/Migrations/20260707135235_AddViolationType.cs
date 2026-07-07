using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingTicketsManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddViolationType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ViolationTypeId",
                table: "ParkingTickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ViolationTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViolationTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingTickets_ViolationTypeId",
                table: "ParkingTickets",
                column: "ViolationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingTickets_ViolationTypes_ViolationTypeId",
                table: "ParkingTickets",
                column: "ViolationTypeId",
                principalTable: "ViolationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingTickets_ViolationTypes_ViolationTypeId",
                table: "ParkingTickets");

            migrationBuilder.DropTable(
                name: "ViolationTypes");

            migrationBuilder.DropIndex(
                name: "IX_ParkingTickets_ViolationTypeId",
                table: "ParkingTickets");

            migrationBuilder.DropColumn(
                name: "ViolationTypeId",
                table: "ParkingTickets");
        }
    }
}
