using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindYourDisease.Users.Infra.Migrations
{
    /// <inheritdoc />
    public partial class addoccupationuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "User",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "User");
        }
    }
}
