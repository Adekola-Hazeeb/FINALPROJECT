using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FINALPROJECT.Migrations
{
    public partial class S2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "01f3c");

            migrationBuilder.AlterColumn<string>(
                name: "ShippingId",
                table: "Payments",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateCreated", "Email", "IsDeleted", "Name", "PasswordHash", "Role", "Salt" },
                values: new object[] { "78867", new DateTime(2024, 8, 28, 1, 28, 29, 192, DateTimeKind.Utc).AddTicks(8630), "admin@gmail.com", false, "admin", "$2a$11$q4w.xgbNdHZXchDz02RqAew35iNpnsDbDeluBPO0EktUTsaRzt4b6", 1, "$2a$11$q4w.xgbNdHZXchDz02RqAe" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "78867");

            migrationBuilder.AlterColumn<string>(
                name: "ShippingId",
                table: "Payments",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateCreated", "Email", "IsDeleted", "Name", "PasswordHash", "Role", "Salt" },
                values: new object[] { "01f3c", new DateTime(2024, 8, 27, 17, 57, 15, 113, DateTimeKind.Utc).AddTicks(9656), "admin@gmail.com", false, "admin", "$2a$11$RpilI4N6g8rrLZXeSziUOeGW.O5odN8aYgGDtY4eTupSrch/O4TIy", 1, "$2a$11$RpilI4N6g8rrLZXeSziUOe" });
        }
    }
}
