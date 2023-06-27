using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotel.API.Data.Migrations
{
    public partial class SeedInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Name" },
                values: new object[,]
                {
                    { "Double room with sea view" },
                    { "Single room with sea view"},
                    { "Double room with garden view"},
                    { "Single room with garden view" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"RoomTypes\"");
        }
    }
}
