using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thing.Migrations
{
    public partial class OptionalProsAndCons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Pros",
                table: "Comments",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Comments",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 10, 40, 31, 269, DateTimeKind.Local).AddTicks(9408),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldDefaultValue: new DateTime(2023, 1, 8, 10, 10, 49, 958, DateTimeKind.Local).AddTicks(43));

            migrationBuilder.AlterColumn<string>(
                name: "Cons",
                table: "Comments",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Pros",
                table: "Comments",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Comments",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 10, 10, 49, 958, DateTimeKind.Local).AddTicks(43),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldDefaultValue: new DateTime(2023, 1, 8, 10, 40, 31, 269, DateTimeKind.Local).AddTicks(9408));

            migrationBuilder.AlterColumn<string>(
                name: "Cons",
                table: "Comments",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}