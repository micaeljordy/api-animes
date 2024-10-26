using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Animes.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoResumoAnime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Resumo",
                table: "Animes",
                type: "varchar(10000)",
                maxLength: 10000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10000)",
                oldMaxLength: 10000)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Resumo",
                keyValue: null,
                column: "Resumo",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Resumo",
                table: "Animes",
                type: "varchar(10000)",
                maxLength: 10000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10000)",
                oldMaxLength: 10000,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
