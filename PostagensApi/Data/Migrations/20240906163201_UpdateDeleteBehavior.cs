using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostagensApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_User_IdUsuario",
                table: "Likes");

            migrationBuilder.AlterColumn<long>(
                name: "AuthorId",
                table: "Post",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Post_AuthorId",
                table: "Post",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_User_IdUsuario",
                table: "Likes",
                column: "IdUsuario",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_User_AuthorId",
                table: "Post",
                column: "AuthorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_User_IdUsuario",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_User_AuthorId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_AuthorId",
                table: "Post");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Post",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_User_IdUsuario",
                table: "Likes",
                column: "IdUsuario",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
