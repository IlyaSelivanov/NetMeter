using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NMeter.Api.Settings.Migrations
{
    public partial class Step_Order_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Steps",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Steps");
        }
    }
}
