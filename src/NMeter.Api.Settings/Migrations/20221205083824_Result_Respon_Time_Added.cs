using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NMeter.Api.Settings.Migrations
{
    public partial class Result_Respon_Time_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ResponseTime",
                table: "Results",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponseTime",
                table: "Results");
        }
    }
}
