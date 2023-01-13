using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exptracker2.Migrations
{
    /// <inheritdoc />
    public partial class secondmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    ExpId = table.Column<int>(name: "Exp_Id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Expensedate = table.Column<DateTime>(name: "Expense_date", type: "datetime2", nullable: false),
                    Amt = table.Column<int>(type: "int", nullable: false),
                    CatId = table.Column<int>(name: "Cat_Id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.ExpId);
                    table.ForeignKey(
                        name: "FK_Expenses_Categories_Cat_Id",
                        column: x => x.CatId,
                        principalTable: "Categories",
                        principalColumn: "Cat_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TotalExplims",
                columns: table => new
                {
                    ExpLimitId = table.Column<int>(name: "ExpLimit_Id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseLimitAmt = table.Column<int>(name: "Expense_Limit_Amt", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalExplims", x => x.ExpLimitId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_Cat_Id",
                table: "Expenses",
                column: "Cat_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "TotalExplims");
        }
    }
}
