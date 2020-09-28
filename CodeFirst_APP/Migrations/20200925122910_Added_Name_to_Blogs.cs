using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeFirst_APP.Migrations
{
    public partial class Added_Name_to_Blogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Blogs",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Blogs");
        }
    }
}
