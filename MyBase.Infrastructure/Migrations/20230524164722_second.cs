using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBase.Infrastructure.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_AspNetUsers_AppUserId",
                table: "BorrowedBooks");

            migrationBuilder.DropIndex(
                name: "IX_BorrowedBooks_AppUserId",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "BorrowedBooks");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "BorrowedBooks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId1",
                table: "BorrowedBooks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "BorrowDate",
                table: "BorrowedBooks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "BorrowedBooks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Fines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fines_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_AppUserId1",
                table: "BorrowedBooks",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Fines_AppUserId",
                table: "Fines",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_AspNetUsers_AppUserId1",
                table: "BorrowedBooks",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_AspNetUsers_AppUserId1",
                table: "BorrowedBooks");

            migrationBuilder.DropTable(
                name: "Fines");

            migrationBuilder.DropIndex(
                name: "IX_BorrowedBooks_AppUserId1",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "BorrowDate",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "BorrowedBooks");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppUserId",
                table: "BorrowedBooks",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "BorrowedBooks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_AppUserId",
                table: "BorrowedBooks",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_AspNetUsers_AppUserId",
                table: "BorrowedBooks",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
