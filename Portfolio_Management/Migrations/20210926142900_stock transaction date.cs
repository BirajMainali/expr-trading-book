using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Portfolio_Management.Migrations
{
    public partial class stocktransactiondate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                schema: "Stock",
                table: "stock",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionDate",
                schema: "Stock",
                table: "stock");
        }
    }
}
