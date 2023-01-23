using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thing.Migrations
{
    public partial class Reinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Comments",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 7, 11, 39, 3, 108, DateTimeKind.Local).AddTicks(8312),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldDefaultValue: new DateTime(2023, 1, 7, 11, 32, 8, 354, DateTimeKind.Local).AddTicks(5607));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Comments",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 7, 11, 32, 8, 354, DateTimeKind.Local).AddTicks(5607),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldDefaultValue: new DateTime(2023, 1, 7, 11, 39, 3, 108, DateTimeKind.Local).AddTicks(8312));
        }
    }
}