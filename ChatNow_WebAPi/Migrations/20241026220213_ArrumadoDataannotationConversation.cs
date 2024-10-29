using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatNow_WebAPi.Migrations
{
    /// <inheritdoc />
    public partial class ArrumadoDataannotationConversation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Friendships_FriendshipIdFriendship",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_FriendshipIdFriendship",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "FriendshipIdFriendship",
                table: "Conversations");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_IdFriendship",
                table: "Conversations",
                column: "IdFriendship");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Friendships_IdFriendship",
                table: "Conversations",
                column: "IdFriendship",
                principalTable: "Friendships",
                principalColumn: "IdFriendship",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Friendships_IdFriendship",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_IdFriendship",
                table: "Conversations");

            migrationBuilder.AddColumn<Guid>(
                name: "FriendshipIdFriendship",
                table: "Conversations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_FriendshipIdFriendship",
                table: "Conversations",
                column: "FriendshipIdFriendship");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Friendships_FriendshipIdFriendship",
                table: "Conversations",
                column: "FriendshipIdFriendship",
                principalTable: "Friendships",
                principalColumn: "IdFriendship");
        }
    }
}
