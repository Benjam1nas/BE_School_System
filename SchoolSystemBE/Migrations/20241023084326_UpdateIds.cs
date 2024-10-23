using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSystemBE.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TLId",
                table: "TemporaryLogins",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Parents",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "EcId",
                table: "Extracurriculars",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "EmailId",
                table: "Emails",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DocId",
                table: "EmailDocuments",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "EcEnrollId",
                table: "EcEnrollments",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TemporaryLogins",
                newName: "TLId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Parents",
                newName: "ParentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Extracurriculars",
                newName: "EcId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Emails",
                newName: "EmailId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "EmailDocuments",
                newName: "DocId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "EcEnrollments",
                newName: "EcEnrollId");
        }
    }
}
