using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AccountApi.Data.Migrations
{
    public partial class SeedNewRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[,]
                {
                    { Guid.NewGuid().ToString() , "User" , "User".ToUpper(), Guid.NewGuid().ToString()  },
                    { Guid.NewGuid().ToString() , "Admin" , "Admin".ToUpper(), Guid.NewGuid().ToString()  }
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"AspNetRoles\";");
        }
    }
}
