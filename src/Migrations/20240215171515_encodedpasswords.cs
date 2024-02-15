using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace src.Migrations
{
    /// <inheritdoc />
    public partial class encodedpasswords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "member",
                keyColumn: "id",
                keyValue: 1,
                column: "password",
                value: "yQp4jaI+UcREW1GJCAfieA==");

            migrationBuilder.UpdateData(
                table: "member",
                keyColumn: "id",
                keyValue: 2,
                column: "password",
                value: "yQp4jaI+UcREW1GJCAfieA==");

            migrationBuilder.UpdateData(
                table: "member",
                keyColumn: "id",
                keyValue: 3,
                column: "password",
                value: "yQp4jaI+UcREW1GJCAfieA==");

            migrationBuilder.UpdateData(
                table: "member",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "first_name", "last_name", "password" },
                values: new object[] { "Fio", "Salas", "yQp4jaI+UcREW1GJCAfieA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "member",
                keyColumn: "id",
                keyValue: 1,
                column: "password",
                value: "123");

            migrationBuilder.UpdateData(
                table: "member",
                keyColumn: "id",
                keyValue: 2,
                column: "password",
                value: "123");

            migrationBuilder.UpdateData(
                table: "member",
                keyColumn: "id",
                keyValue: 3,
                column: "password",
                value: "123");

            migrationBuilder.UpdateData(
                table: "member",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "first_name", "last_name", "password" },
                values: new object[] { "Jorge", "González", "123" });
        }
    }
}
