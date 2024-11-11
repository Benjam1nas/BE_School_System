using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSystemBE.Migrations
{
    /// <inheritdoc />
    public partial class AddFKs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentsParents_Parents_ParentId",
                table: "StudentsParents");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsParents_Students_StudentId",
                table: "StudentsParents");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryLogins_UserId",
                table: "TemporaryLogins",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parents_UserId",
                table: "Parents",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EcEnrollments_EcId",
                table: "EcEnrollments",
                column: "EcId");

            migrationBuilder.CreateIndex(
                name: "IX_EcEnrollments_StudentId",
                table: "EcEnrollments",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EcEnrollments_Extracurriculars_EcId",
                table: "EcEnrollments",
                column: "EcId",
                principalTable: "Extracurriculars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EcEnrollments_Students_StudentId",
                table: "EcEnrollments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Users_UserId",
                table: "Parents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Users_UserId",
                table: "Students",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsParents_Parents_ParentId",
                table: "StudentsParents",
                column: "ParentId",
                principalTable: "Parents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsParents_Students_StudentId",
                table: "StudentsParents",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TemporaryLogins_Users_UserId",
                table: "TemporaryLogins",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EcEnrollments_Extracurriculars_EcId",
                table: "EcEnrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_EcEnrollments_Students_StudentId",
                table: "EcEnrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Users_UserId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Users_UserId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsParents_Parents_ParentId",
                table: "StudentsParents");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsParents_Students_StudentId",
                table: "StudentsParents");

            migrationBuilder.DropForeignKey(
                name: "FK_TemporaryLogins_Users_UserId",
                table: "TemporaryLogins");

            migrationBuilder.DropIndex(
                name: "IX_TemporaryLogins_UserId",
                table: "TemporaryLogins");

            migrationBuilder.DropIndex(
                name: "IX_Students_UserId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Parents_UserId",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_EcEnrollments_EcId",
                table: "EcEnrollments");

            migrationBuilder.DropIndex(
                name: "IX_EcEnrollments_StudentId",
                table: "EcEnrollments");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsParents_Parents_ParentId",
                table: "StudentsParents",
                column: "ParentId",
                principalTable: "Parents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsParents_Students_StudentId",
                table: "StudentsParents",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
