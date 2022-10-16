using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAC.DbMigrations.Migrations
{
    public class IdentityConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddPrimaryKey("PK_AspNetRoles", "AspNetRoles", "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
