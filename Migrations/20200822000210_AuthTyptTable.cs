using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AuthorizationAPI.Migrations
{
    public partial class AuthTyptTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthenticationTypeId",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AuthenticationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AuthenticationTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthenticationTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AuthenticationTypeId",
                table: "Users",
                column: "AuthenticationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AuthenticationTypes_AuthenticationTypeId",
                table: "Users",
                column: "AuthenticationTypeId",
                principalTable: "AuthenticationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_AuthenticationTypes_AuthenticationTypeId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "AuthenticationTypes");

            migrationBuilder.DropIndex(
                name: "IX_Users_AuthenticationTypeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AuthenticationTypeId",
                table: "Users");
        }
    }
}
