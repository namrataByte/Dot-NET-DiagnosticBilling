using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiagnosticBilling.Migrations
{
    /// <inheritdoc />
    public partial class AddBillTestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillTests_Bill_BillId",
                table: "BillTests");

            migrationBuilder.DropForeignKey(
                name: "FK_BillTests_Test_TestId",
                table: "BillTests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillTests",
                table: "BillTests");

            migrationBuilder.RenameTable(
                name: "BillTests",
                newName: "BillTest");

            migrationBuilder.RenameIndex(
                name: "IX_BillTests_TestId",
                table: "BillTest",
                newName: "IX_BillTest_TestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillTest",
                table: "BillTest",
                columns: new[] { "BillId", "TestId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BillTest_Bill_BillId",
                table: "BillTest",
                column: "BillId",
                principalTable: "Bill",
                principalColumn: "billId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillTest_Test_TestId",
                table: "BillTest",
                column: "TestId",
                principalTable: "Test",
                principalColumn: "TestId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillTest_Bill_BillId",
                table: "BillTest");

            migrationBuilder.DropForeignKey(
                name: "FK_BillTest_Test_TestId",
                table: "BillTest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillTest",
                table: "BillTest");

            migrationBuilder.RenameTable(
                name: "BillTest",
                newName: "BillTests");

            migrationBuilder.RenameIndex(
                name: "IX_BillTest_TestId",
                table: "BillTests",
                newName: "IX_BillTests_TestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillTests",
                table: "BillTests",
                columns: new[] { "BillId", "TestId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BillTests_Bill_BillId",
                table: "BillTests",
                column: "BillId",
                principalTable: "Bill",
                principalColumn: "billId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillTests_Test_TestId",
                table: "BillTests",
                column: "TestId",
                principalTable: "Test",
                principalColumn: "TestId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
