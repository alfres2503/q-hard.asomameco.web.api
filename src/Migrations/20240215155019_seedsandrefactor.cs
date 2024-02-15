using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace src.Migrations
{
    /// <inheritdoc />
    public partial class seedsandrefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "arrival_time",
                table: "attendance",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time");

            migrationBuilder.InsertData(
                table: "associate",
                columns: new[] { "id", "email", "id_card", "is_active", "name", "phone" },
                values: new object[,]
                {
                    { 1, "luis@mail.com", "555555555", true, "Luis Gallego", "88888888" },
                    { 2, "mario@mail.com", "666666666", true, "Mario Gallego", "77777777" },
                    { 3, "pfer@mail.com", "777777777", true, "Pedro Fernández", "66666666" }
                });

            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "id", "description" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Member" }
                });

            migrationBuilder.InsertData(
                table: "member",
                columns: new[] { "id", "email", "first_name", "id_card", "id_role", "is_active", "last_name", "password" },
                values: new object[,]
                {
                    { 1, "lusuarezag@est.utn.ac.cr", "Fred", "111111111", 1, true, "Suárez", "123" },
                    { 2, "malopezsa@est.utn.ac.cr", "Pala", "222222222", 2, true, "López", "123" },
                    { 3, "gabulatem@est.utn.ac.cr", "Gabo", "333333333", 2, true, "Ulate", "123" },
                    { 4, "jgonzalez@mail.com", "Jorge", "444444444", 2, false, "González", "123" }
                });

            migrationBuilder.InsertData(
                table: "event",
                columns: new[] { "id", "date", "description", "id_member", "name", "place", "time" },
                values: new object[,]
                {
                    { 1, new DateOnly(2022, 10, 21), "Reunión de la junta directiva para discutir temas importantes", 2, "Reunión de la junta directiva", "Sala de juntas", new TimeOnly(7, 23, 11) },
                    { 2, new DateOnly(2022, 12, 23), "Fiestón para esuchar Luis Miguel", 3, "Fiesta Mariachi", "Sala de juntas", new TimeOnly(10, 30, 11) },
                    { 3, new DateOnly(2023, 1, 15), "Reunión recapacitativa", 2, "Reunión porqué amo a mi esposita", "Sala de juntas", new TimeOnly(14, 0, 11) }
                });

            migrationBuilder.InsertData(
                table: "attendance",
                columns: new[] { "id_associate", "id_event", "arrival_time", "is_confirmed" },
                values: new object[,]
                {
                    { 1, 1, new TimeOnly(7, 23, 11), true },
                    { 1, 2, new TimeOnly(10, 30, 11), true },
                    { 1, 3, null, false },
                    { 2, 1, new TimeOnly(7, 23, 11), true },
                    { 2, 2, new TimeOnly(10, 32, 11), true },
                    { 2, 3, new TimeOnly(14, 0, 11), true },
                    { 3, 1, new TimeOnly(7, 23, 11), true },
                    { 3, 2, null, false },
                    { 3, 3, new TimeOnly(14, 0, 11), true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "attendance",
                keyColumns: new[] { "id_associate", "id_event" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "attendance",
                keyColumns: new[] { "id_associate", "id_event" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "attendance",
                keyColumns: new[] { "id_associate", "id_event" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "attendance",
                keyColumns: new[] { "id_associate", "id_event" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "attendance",
                keyColumns: new[] { "id_associate", "id_event" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "attendance",
                keyColumns: new[] { "id_associate", "id_event" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "attendance",
                keyColumns: new[] { "id_associate", "id_event" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "attendance",
                keyColumns: new[] { "id_associate", "id_event" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "attendance",
                keyColumns: new[] { "id_associate", "id_event" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "member",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "member",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "associate",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "associate",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "associate",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "event",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "event",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "event",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "member",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "member",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "arrival_time",
                table: "attendance",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0),
                oldClrType: typeof(TimeOnly),
                oldType: "time",
                oldNullable: true);
        }
    }
}
