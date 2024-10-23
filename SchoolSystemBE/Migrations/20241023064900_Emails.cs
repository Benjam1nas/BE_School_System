using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSystemBE.Migrations
{
    /// <inheritdoc />
    public partial class Emails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EcEnrollment",
                table: "EcEnrollment");

            migrationBuilder.RenameTable(
                name: "EcEnrollment",
                newName: "EcEnrollments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EcEnrollments",
                table: "EcEnrollments",
                column: "EcEnrollId");

            migrationBuilder.CreateTable(
                name: "EmailDocuments",
                columns: table => new
                {
                    DocId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UploadedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailDocuments", x => x.DocId);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    EmailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    References = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.EmailId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailDocuments");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EcEnrollments",
                table: "EcEnrollments");

            migrationBuilder.RenameTable(
                name: "EcEnrollments",
                newName: "EcEnrollment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EcEnrollment",
                table: "EcEnrollment",
                column: "EcEnrollId");
        }
    }
}
