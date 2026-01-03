using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeedBackManageSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusAndDateCreatedToFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "tblFeedback",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "tblFeedback",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "tblFeedback");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "tblFeedback");
        }
    }
}
