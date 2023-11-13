using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindYourDisease.Patient.Migrations
{
    /// <inheritdoc />
    public partial class addactivecolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Patients",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Patients");
        }
    }
}
