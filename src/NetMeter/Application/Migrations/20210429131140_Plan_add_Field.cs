using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class Plan_add_Field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Field",
                table: "Plans",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Field",
                table: "Plans");
        }
    }
}
