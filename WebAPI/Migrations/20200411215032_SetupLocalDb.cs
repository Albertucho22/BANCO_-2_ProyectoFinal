// using System;
// using Microsoft.EntityFrameworkCore.Migrations;

// namespace WebAPI.Migrations
// {
//     public partial class SetupLocalDb : Migration
//     {
//         protected override void Up(MigrationBuilder migrationBuilder)
//         {
//             migrationBuilder.CreateTable(
//                 name: "Admins",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(nullable: false)
//                         .Annotation("SqlServer:Identity", "1, 1"),
//                     FirstName = table.Column<string>(maxLength: 50, nullable: false),
//                     LastName = table.Column<string>(maxLength: 50, nullable: false),
//                     UserName = table.Column<string>(maxLength: 50, nullable: false),
//                     Password = table.Column<string>(maxLength: 50, nullable: false),
//                     Email = table.Column<string>(maxLength: 50, nullable: false),
//                     CreatedAt = table.Column<DateTime>(nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_Admins", x => x.Id);
//                 });

//             migrationBuilder.CreateTable(
//                 name: "Clients",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(nullable: false)
//                         .Annotation("SqlServer:Identity", "1, 1"),
//                     FirstName = table.Column<string>(maxLength: 50, nullable: false),
//                     LastName = table.Column<string>(maxLength: 50, nullable: false),
//                     UserName = table.Column<string>(maxLength: 50, nullable: false),
//                     Password = table.Column<string>(maxLength: 50, nullable: false),
//                     Email = table.Column<string>(maxLength: 50, nullable: false),
//                     NationalId = table.Column<string>(maxLength: 50, nullable: false),
//                     CreatedAt = table.Column<DateTime>(nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_Clients", x => x.Id);
//                 });

//             migrationBuilder.CreateTable(
//                 name: "DataConsultants",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(nullable: false)
//                         .Annotation("SqlServer:Identity", "1, 1"),
//                     FirstName = table.Column<string>(maxLength: 50, nullable: false),
//                     LastName = table.Column<string>(maxLength: 50, nullable: false),
//                     UserName = table.Column<string>(maxLength: 50, nullable: false),
//                     Password = table.Column<string>(maxLength: 50, nullable: false),
//                     Email = table.Column<string>(maxLength: 50, nullable: false),
//                     CreatedAt = table.Column<DateTime>(nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_DataConsultants", x => x.Id);
//                 });

//             migrationBuilder.CreateTable(
//                 name: "DataMaintainers",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(nullable: false)
//                         .Annotation("SqlServer:Identity", "1, 1"),
//                     FirstName = table.Column<string>(maxLength: 50, nullable: false),
//                     LastName = table.Column<string>(maxLength: 50, nullable: false),
//                     UserName = table.Column<string>(maxLength: 50, nullable: false),
//                     Password = table.Column<string>(maxLength: 50, nullable: false),
//                     Email = table.Column<string>(maxLength: 50, nullable: false),
//                     CreatedAt = table.Column<DateTime>(nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_DataMaintainers", x => x.Id);
//                 });

//             migrationBuilder.CreateTable(
//                 name: "Accounts",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(nullable: false)
//                         .Annotation("SqlServer:Identity", "1, 1"),
//                     CreatedAt = table.Column<DateTime>(nullable: false),
//                     Balance = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
//                     ClientId = table.Column<int>(nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_Accounts", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_Accounts_Clients_ClientId",
//                         column: x => x.ClientId,
//                         principalTable: "Clients",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });

//             migrationBuilder.CreateTable(
//                 name: "Loan",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(nullable: false)
//                         .Annotation("SqlServer:Identity", "1, 1"),
//                     YearlyInterestRate = table.Column<float>(nullable: false),
//                     DurationInMonths = table.Column<int>(nullable: false),
//                     TotalAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
//                     AccountId = table.Column<int>(nullable: false),
//                     Information = table.Column<string>(nullable: true),
//                     MonthlyPayment = table.Column<double>(nullable: false),
//                     RemainingAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_Loan", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_Loan_Accounts_AccountId",
//                         column: x => x.AccountId,
//                         principalTable: "Accounts",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });

//             migrationBuilder.CreateTable(
//                 name: "Transactions",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(nullable: false)
//                         .Annotation("SqlServer:Identity", "1, 1"),
//                     CreatedAt = table.Column<DateTime>(nullable: false),
//                     Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
//                     Information = table.Column<string>(maxLength: 100, nullable: false),
//                     AccountId = table.Column<int>(nullable: false),
//                     Discriminator = table.Column<string>(nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_Transactions", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_Transactions_Accounts_AccountId",
//                         column: x => x.AccountId,
//                         principalTable: "Accounts",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.NoAction);
//                 });

//             migrationBuilder.CreateIndex(
//                 name: "IX_Accounts_ClientId",
//                 table: "Accounts",
//                 column: "ClientId");

//             migrationBuilder.CreateIndex(
//                 name: "IX_Loan_AccountId",
//                 table: "Loan",
//                 column: "AccountId");

//             migrationBuilder.CreateIndex(
//                 name: "IX_Transactions_AccountId",
//                 table: "Transactions",
//                 column: "AccountId");
//         }

//         protected override void Down(MigrationBuilder migrationBuilder)
//         {
//             migrationBuilder.DropTable(
//                 name: "Admins");

//             migrationBuilder.DropTable(
//                 name: "DataConsultants");

//             migrationBuilder.DropTable(
//                 name: "DataMaintainers");

//             migrationBuilder.DropTable(
//                 name: "Loan");

//             migrationBuilder.DropTable(
//                 name: "Transactions");

//             migrationBuilder.DropTable(
//                 name: "Accounts");

//             migrationBuilder.DropTable(
//                 name: "Clients");
//         }
//     }
// }
