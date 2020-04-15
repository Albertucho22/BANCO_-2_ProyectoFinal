using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class AddLoanPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoanId",
                table: "Transactions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_LoanId",
                table: "Transactions",
                column: "LoanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Loan_LoanId",
                table: "Transactions",
                column: "LoanId",
                principalTable: "Loan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Loan_LoanId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_LoanId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "LoanId",
                table: "Transactions");
        }
    }
}
