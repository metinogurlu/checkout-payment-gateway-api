using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PaymentGateway.API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    process_id = table.Column<Guid>(nullable: true),
                    card_number = table.Column<string>(nullable: true),
                    amount = table.Column<decimal>(nullable: false),
                    currency = table.Column<string>(nullable: true),
                    response_code = table.Column<int>(nullable: false),
                    response_summary = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    processed_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payments");
        }
    }
}
