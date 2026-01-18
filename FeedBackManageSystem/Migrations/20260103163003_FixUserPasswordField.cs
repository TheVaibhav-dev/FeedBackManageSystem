using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeedBackManageSystem.Migrations
{
    /// <inheritdoc />
    public partial class FixUserPasswordField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswowordHash",
                table: "tblUser",
                newName: "PasswordHash");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "tblUser",
                newName: "PasswowordHash");
        }
    }
}
