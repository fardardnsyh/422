using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogedium_api.Migrations
{
    /// <inheritdoc />
    public partial class readcountfiledadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReadCount",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadCount",
                table: "Blogs");
        }
    }
}
