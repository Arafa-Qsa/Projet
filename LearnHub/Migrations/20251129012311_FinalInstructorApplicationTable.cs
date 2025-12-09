using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnHub.Migrations
{
    /// <inheritdoc />
    public partial class FinalInstructorApplicationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_InstructorApplications_InstructorApplicationId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_InstructorApplications_InstructorApplicationId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_InstructorApplications_InstructorApplications_InstructorApplicationId",
                table: "InstructorApplications");

            migrationBuilder.DropIndex(
                name: "IX_InstructorApplications_InstructorApplicationId",
                table: "InstructorApplications");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_InstructorApplicationId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Courses_InstructorApplicationId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "InstructorApplicationId",
                table: "InstructorApplications");

            migrationBuilder.DropColumn(
                name: "InstructorApplicationId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "InstructorApplicationId",
                table: "Courses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstructorApplicationId",
                table: "InstructorApplications",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstructorApplicationId",
                table: "Enrollments",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstructorApplicationId",
                table: "Courses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstructorApplications_InstructorApplicationId",
                table: "InstructorApplications",
                column: "InstructorApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_InstructorApplicationId",
                table: "Enrollments",
                column: "InstructorApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_InstructorApplicationId",
                table: "Courses",
                column: "InstructorApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_InstructorApplications_InstructorApplicationId",
                table: "Courses",
                column: "InstructorApplicationId",
                principalTable: "InstructorApplications",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_InstructorApplications_InstructorApplicationId",
                table: "Enrollments",
                column: "InstructorApplicationId",
                principalTable: "InstructorApplications",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InstructorApplications_InstructorApplications_InstructorApplicationId",
                table: "InstructorApplications",
                column: "InstructorApplicationId",
                principalTable: "InstructorApplications",
                principalColumn: "Id");
        }
    }
}
