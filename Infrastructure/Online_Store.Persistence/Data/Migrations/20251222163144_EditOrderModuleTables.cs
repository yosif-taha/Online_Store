using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Store.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditOrderModuleTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Orders",
                newName: "UserEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Orders",
                newName: "UserName");
        }
    }
}
