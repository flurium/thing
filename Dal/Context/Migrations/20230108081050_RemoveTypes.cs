using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dal.Migrations
{
    public partial class RemoveTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(300)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Comments",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 10, 10, 49, 958, DateTimeKind.Local).AddTicks(43),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldDefaultValue: new DateTime(2023, 1, 7, 11, 39, 3, 108, DateTimeKind.Local).AddTicks(8312));

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Comments",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(300)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nchar(300)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Comments",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 7, 11, 39, 3, 108, DateTimeKind.Local).AddTicks(8312),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldDefaultValue: new DateTime(2023, 1, 8, 10, 10, 49, 958, DateTimeKind.Local).AddTicks(43));

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Comments",
                type: "nchar(300)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}