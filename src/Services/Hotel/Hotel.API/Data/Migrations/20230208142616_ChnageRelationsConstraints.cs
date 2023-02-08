using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotel.API.Data.Migrations
{
    public partial class ChnageRelationsConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Hotel",
                table: "Rooms");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Hotel",
                table: "Rooms",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Hotel",
                table: "Rooms");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Hotel",
                table: "Rooms",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
