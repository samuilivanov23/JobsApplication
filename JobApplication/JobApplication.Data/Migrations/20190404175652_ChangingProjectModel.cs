using Microsoft.EntityFrameworkCore.Migrations;

namespace JobApplication.Data.Migrations
{
    public partial class ChangingProjectModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_CVs_CVId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "CVId",
                table: "Projects",
                newName: "CvId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_CVId",
                table: "Projects",
                newName: "IX_Projects_CvId");

            migrationBuilder.AlterColumn<int>(
                name: "CvId",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_CVs_CvId",
                table: "Projects",
                column: "CvId",
                principalTable: "CVs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_CVs_CvId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "CvId",
                table: "Projects",
                newName: "CVId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_CvId",
                table: "Projects",
                newName: "IX_Projects_CVId");

            migrationBuilder.AlterColumn<int>(
                name: "CVId",
                table: "Projects",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_CVs_CVId",
                table: "Projects",
                column: "CVId",
                principalTable: "CVs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
