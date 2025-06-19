using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyHub.Migrations
{
    /// <inheritdoc />
    public partial class addisreplyfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReply",
                table: "CommentReply",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ReplyIdOfReply",
                table: "CommentReply",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommentReply_ReplyIdOfReply",
                table: "CommentReply",
                column: "ReplyIdOfReply");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReply_CommentReply_ReplyIdOfReply",
                table: "CommentReply",
                column: "ReplyIdOfReply",
                principalTable: "CommentReply",
                principalColumn: "CommentRply_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReply_CommentReply_ReplyIdOfReply",
                table: "CommentReply");

            migrationBuilder.DropIndex(
                name: "IX_CommentReply_ReplyIdOfReply",
                table: "CommentReply");

            migrationBuilder.DropColumn(
                name: "IsReply",
                table: "CommentReply");

            migrationBuilder.DropColumn(
                name: "ReplyIdOfReply",
                table: "CommentReply");
        }
    }
}
