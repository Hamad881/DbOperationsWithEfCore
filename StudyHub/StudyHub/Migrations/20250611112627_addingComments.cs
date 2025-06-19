using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyHub.Migrations
{
    /// <inheritdoc />
    public partial class addingComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Comment_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Id = table.Column<int>(type: "int", nullable: false),
                    Post_Id = table.Column<int>(type: "int", nullable: false),
                    Comment_Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Comment_Id);
                    table.ForeignKey(
                        name: "FK_Comment_Post_Post_Id",
                        column: x => x.Post_Id,
                        principalTable: "Post",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommentReply",
                columns: table => new
                {
                    CommentRply_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment_Id = table.Column<int>(type: "int", nullable: false),
                    Comment_Reply = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentReply", x => x.CommentRply_Id);
                    table.ForeignKey(
                        name: "FK_CommentReply_Comment_Comment_Id",
                        column: x => x.Comment_Id,
                        principalTable: "Comment",
                        principalColumn: "Comment_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentReply_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_Post_Id",
                table: "Comment",
                column: "Post_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_User_Id",
                table: "Comment",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CommentReply_Comment_Id",
                table: "CommentReply",
                column: "Comment_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CommentReply_User_Id",
                table: "CommentReply",
                column: "User_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentReply");

            migrationBuilder.DropTable(
                name: "Comment");
        }
    }
}
