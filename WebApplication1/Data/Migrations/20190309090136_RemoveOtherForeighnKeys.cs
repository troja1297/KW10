using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Data.Migrations
{
    public partial class RemoveOtherForeighnKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AspNetUsers_UserId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Comments_CommentId",
                table: "Pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Companies_CompanyId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_CommentId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_CompanyId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Companies_UserId",
                table: "Companies");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyId",
                table: "Pictures",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CommentId",
                table: "Pictures",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Companies",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserApplicationId",
                table: "Companies",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_UserApplicationId",
                table: "Companies",
                column: "UserApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AspNetUsers_UserApplicationId",
                table: "Companies",
                column: "UserApplicationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AspNetUsers_UserApplicationId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_UserApplicationId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "UserApplicationId",
                table: "Companies");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyId",
                table: "Pictures",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CommentId",
                table: "Pictures",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Companies",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_CommentId",
                table: "Pictures",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_CompanyId",
                table: "Pictures",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_UserId",
                table: "Companies",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AspNetUsers_UserId",
                table: "Companies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Comments_CommentId",
                table: "Pictures",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Companies_CompanyId",
                table: "Pictures",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
