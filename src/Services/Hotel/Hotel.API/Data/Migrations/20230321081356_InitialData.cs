using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotel.API.Data.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Name", "Address", "Location" },
                values: new object[,]
                {
                    { "Hotel Bel Azur Thalasso & Bungalows" ,"Hammamet" , "Rue Assad Ibn Fourat BP 13, 8050 Hammamet, Tunisia"},
                    { "La Badira - Adult Only , Hammamet" ,"Hammamet" , "8050 Hammamet, Tunisia " },
                    { "El Mouradi El Menzah" ,"Hammamet" , "Zone Touristique Yasmine Hammamet, 8050 Hammamet, Tunisia" },
                    { "Medina Belisaire And Thalasso" ,"Hammamet" , "Bp. 204 8050 Yasmine Hammamet, 8050 Hammamet, Tunisia"},
                    { "Royal Azur Thalassa" ,"Hammamet" , "AVENUE ASSAD IBN FOURAT HAMMAMET, 8050 Hammamet, Tunisia "}
                });

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

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "RoomId", "RoomTypeId", "Floor", "Number", "HotelId", "Name", "IsAvailable" },
                values: new object[,]
                {
                    { 1,2,4,401,3,"SS401",true},
                    { 2,2,2,212,3,"SS212",true},
                    { 3,1,8,803,3,"DS803",true},
                    { 4,3,6,613,3,"DG613",true},
                    { 5,4,3,302,4,"SG302",true},
                    { 6,4,4,416,4,"SG416",true},
                    { 7,2,5,511,4,"SS511",true },
                    { 8,4,6,619,4,"SG619",true },
                    { 9,3,3,303,2,"DG303",true},
                    { 10,1,4,411,2,"DS411",true},
                    { 11,1,2,212,2,"DS212",true},
                    { 12,2,1,109,2,"SS109",true}
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
