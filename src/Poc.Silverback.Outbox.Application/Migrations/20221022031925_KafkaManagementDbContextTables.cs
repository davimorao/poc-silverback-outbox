using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Poc.Silverback.Outbox.Application.Migrations
{
    public partial class KafkaManagementDbContextTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InboundMessages",
                columns: table => new
                {
                    MessageId = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ConsumerGroupName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    EndpointName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Consumed = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboundMessages", x => new { x.MessageId, x.ConsumerGroupName });
                });

            migrationBuilder.CreateTable(
                name: "Locks",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UniqueId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Heartbeat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locks", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Outbox",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Headers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerializedHeaders = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    EndpointName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ActualEndpointName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outbox", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoredOffsets",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ClrType = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Offset = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoredOffsets", x => x.Key);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InboundMessages");

            migrationBuilder.DropTable(
                name: "Locks");

            migrationBuilder.DropTable(
                name: "Outbox");

            migrationBuilder.DropTable(
                name: "StoredOffsets");
        }
    }
}
