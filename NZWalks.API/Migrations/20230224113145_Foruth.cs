using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class Foruth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walks_WalksDiffuculty_WalkDiffucultyId",
                table: "Walks");

            migrationBuilder.DropIndex(
                name: "IX_Walks_WalkDiffucultyId",
                table: "Walks");

            migrationBuilder.DropColumn(
                name: "WalkDiffucultyId",
                table: "Walks");

            migrationBuilder.CreateIndex(
                name: "IX_Walks_WalkDifficultyId",
                table: "Walks",
                column: "WalkDifficultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_WalksDiffuculty_WalkDifficultyId",
                table: "Walks",
                column: "WalkDifficultyId",
                principalTable: "WalksDiffuculty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walks_WalksDiffuculty_WalkDifficultyId",
                table: "Walks");

            migrationBuilder.DropIndex(
                name: "IX_Walks_WalkDifficultyId",
                table: "Walks");

            migrationBuilder.AddColumn<Guid>(
                name: "WalkDiffucultyId",
                table: "Walks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Walks_WalkDiffucultyId",
                table: "Walks",
                column: "WalkDiffucultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_WalksDiffuculty_WalkDiffucultyId",
                table: "Walks",
                column: "WalkDiffucultyId",
                principalTable: "WalksDiffuculty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
