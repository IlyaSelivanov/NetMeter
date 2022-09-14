using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NMeter.Api.Settings.Migrations
{
    public partial class Execution_Model_PlanId_Naming_Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Execution_Plans_planId",
                table: "Execution");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Execution",
                table: "Execution");

            migrationBuilder.RenameTable(
                name: "Execution",
                newName: "Executions");

            migrationBuilder.RenameColumn(
                name: "planId",
                table: "Executions",
                newName: "PlanId");

            migrationBuilder.RenameIndex(
                name: "IX_Execution_planId",
                table: "Executions",
                newName: "IX_Executions_PlanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Executions",
                table: "Executions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Executions_Plans_PlanId",
                table: "Executions",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Executions_Plans_PlanId",
                table: "Executions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Executions",
                table: "Executions");

            migrationBuilder.RenameTable(
                name: "Executions",
                newName: "Execution");

            migrationBuilder.RenameColumn(
                name: "PlanId",
                table: "Execution",
                newName: "planId");

            migrationBuilder.RenameIndex(
                name: "IX_Executions_PlanId",
                table: "Execution",
                newName: "IX_Execution_planId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Execution",
                table: "Execution",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Execution_Plans_planId",
                table: "Execution",
                column: "planId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
