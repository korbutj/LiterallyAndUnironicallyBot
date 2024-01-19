using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bot.Data.Migrations
{
    /// <inheritdoc />
    public partial class nastepnamigracja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_GuildSettings_GuildId",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_GuildId",
                table: "Quotes");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Quotes",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)");

            migrationBuilder.AddColumn<Guid>(
                name: "GuildId1",
                table: "Quotes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "QuoteId",
                table: "QuoteAttachments",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "QuoteAttachments",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "GuildSettings",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)");

            migrationBuilder.AddColumn<decimal>(
                name: "GuildId",
                table: "GuildSettings",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_GuildId1",
                table: "Quotes",
                column: "GuildId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_GuildSettings_GuildId1",
                table: "Quotes",
                column: "GuildId1",
                principalTable: "GuildSettings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_GuildSettings_GuildId1",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_GuildId1",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "GuildId1",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "GuildId",
                table: "GuildSettings");

            migrationBuilder.AlterColumn<decimal>(
                name: "Id",
                table: "Quotes",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<decimal>(
                name: "QuoteId",
                table: "QuoteAttachments",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<decimal>(
                name: "Id",
                table: "QuoteAttachments",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<decimal>(
                name: "Id",
                table: "GuildSettings",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_GuildId",
                table: "Quotes",
                column: "GuildId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_GuildSettings_GuildId",
                table: "Quotes",
                column: "GuildId",
                principalTable: "GuildSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
