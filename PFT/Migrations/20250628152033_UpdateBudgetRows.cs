using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFT.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBudgetRows : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Budgets",
                newName: "MaxAmount");

            migrationBuilder.AddColumn<int>(
                name: "CurrentlySpent",
                table: "Budgets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentlySpent",
                table: "Budgets");

            migrationBuilder.RenameColumn(
                name: "MaxAmount",
                table: "Budgets",
                newName: "Amount");
        }
    }
}
