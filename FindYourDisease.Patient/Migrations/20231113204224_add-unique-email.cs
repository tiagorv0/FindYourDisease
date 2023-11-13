using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindYourDisease.Patient.Migrations
{
    /// <inheritdoc />
    public partial class adduniqueemail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Patients_Email",
                table: "Patients",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patients_Email",
                table: "Patients");
        }
    }
}
