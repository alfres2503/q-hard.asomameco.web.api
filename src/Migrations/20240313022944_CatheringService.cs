using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace src.Migrations
{
    /// <inheritdoc />
    public partial class CatheringService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_catering_service",
                table: "event",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "catering_service",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_catering_service", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "catering_service",
                columns: new[] { "id", "email", "is_active", "name", "phone" },
                values: new object[,]
                {
                    { 1, "info@cateringsoluciones.com", true, "Catering Soluciones", "2271 7575" },
                    { 2, "info@lacucharaderebe.com", true, "Servicio & Sazón - Catering Service", "7150 9328" },
                    { 3, "pastaprontocr@gmail.com", true, "Pasta Pronto Express y Catering", "4704 8057" },
                    { 4, "info@newrest.com", true, "Newrest", "2437 1768" }
                });

            migrationBuilder.UpdateData(
                table: "event",
                keyColumn: "id",
                keyValue: 1,
                column: "id_catering_service",
                value: 1);

            migrationBuilder.UpdateData(
                table: "event",
                keyColumn: "id",
                keyValue: 2,
                column: "id_catering_service",
                value: 3);

            migrationBuilder.UpdateData(
                table: "event",
                keyColumn: "id",
                keyValue: 3,
                column: "id_catering_service",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "ix_event_id_catering_service",
                table: "event",
                column: "id_catering_service");

            migrationBuilder.AddForeignKey(
                name: "fk_event_catering_service_id_catering_service",
                table: "event",
                column: "id_catering_service",
                principalTable: "catering_service",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_event_catering_service_id_catering_service",
                table: "event");

            migrationBuilder.DropTable(
                name: "catering_service");

            migrationBuilder.DropIndex(
                name: "ix_event_id_catering_service",
                table: "event");

            migrationBuilder.DropColumn(
                name: "id_catering_service",
                table: "event");
        }
    }
}
