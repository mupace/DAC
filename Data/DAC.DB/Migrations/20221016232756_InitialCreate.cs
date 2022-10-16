using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAC.DB.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Workorders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Responsible = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValue: DateTime.UtcNow),
                    UpdateDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workorders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    WorkOrderId = table.Column<Guid>(nullable: false),
                    Note = table.Column<string>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValue: DateTime.UtcNow)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrderNotes_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
