using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfotecsIntershipMVC.Migrations
{
    /// <inheritdoc />
    public partial class File121ResultRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Values_Files_FileId",
                table: "Values");

            migrationBuilder.RenameColumn(
                name: "FileId",
                table: "Values",
                newName: "FileID");

            migrationBuilder.RenameIndex(
                name: "IX_Values_FileId",
                table: "Values",
                newName: "IX_Values_FileID");

            migrationBuilder.AddColumn<Guid>(
                name: "FileID",
                table: "Results",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Results_FileID",
                table: "Results",
                column: "FileID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Files_FileID",
                table: "Results",
                column: "FileID",
                principalTable: "Files",
                principalColumn: "FileID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Values_Files_FileID",
                table: "Values",
                column: "FileID",
                principalTable: "Files",
                principalColumn: "FileID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Files_FileID",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Values_Files_FileID",
                table: "Values");

            migrationBuilder.DropIndex(
                name: "IX_Results_FileID",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "FileID",
                table: "Results");

            migrationBuilder.RenameColumn(
                name: "FileID",
                table: "Values",
                newName: "FileId");

            migrationBuilder.RenameIndex(
                name: "IX_Values_FileID",
                table: "Values",
                newName: "IX_Values_FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Values_Files_FileId",
                table: "Values",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "FileID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
