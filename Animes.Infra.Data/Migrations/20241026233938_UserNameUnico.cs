using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Animes.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserNameUnico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_UserName",
                table: "Usuarios",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_UserName",
                table: "Usuarios");
        }
    }
}
