using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeesDashboard.Migrations
{
    public partial class InsertedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4672f4d1-50ef-4515-a500-db62de28658f", "451bc958-04af-4712-8783-c5331e627b7b", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cb78b2a2-6940-4cc2-90a6-ae3dca1224d1", "ec2b6f17-8f2f-492a-bfba-4cf3f70966c6", "DataMaintainer", "DATAMAINTAINER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0fc83d9d-6e7c-4d75-b1f7-c6f46bf1047d", "f820fc35-7adb-49b0-b0ee-96ba723f4b44", "DataConsultant", "DATACONSULTANT" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0fc83d9d-6e7c-4d75-b1f7-c6f46bf1047d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4672f4d1-50ef-4515-a500-db62de28658f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb78b2a2-6940-4cc2-90a6-ae3dca1224d1");
        }
    }
}
