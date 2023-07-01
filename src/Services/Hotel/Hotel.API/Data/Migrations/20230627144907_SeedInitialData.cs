using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotel.API.Data.Migrations
{
    public partial class SeedInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"Rooms\";");
            migrationBuilder.Sql("DELETE FROM \"Hotels\";");
            migrationBuilder.Sql("DELETE FROM \"RoomTypes\";");

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
                table: "Hotels",
                columns: new[] { "HotelId", "Name" ,"Address","Location" },
                values: new object[,]
                {
                    {"e2be74e4-d5d3-4153-a87b-6780a2af4dc5", "Hotel Bel Azur Thalasso & Bungalows" ,"Hammamet" , "Rue Assad Ibn Fourat BP 13, 8050 Hammamet, Tunisia"},
                    {"4e6ea44e-6b9e-43f2-890d-00e2e13e273b", "La Badira - Adult Only , Hammamet" ,"Hammamet" , "8050 Hammamet, Tunisia "},
                    {"2b4314f7-5f2a-4ee3-8e39-49860d3e2275", "El Mouradi El Menzah" ,"Hammamet" , "Zone Touristique Yasmine Hammamet, 8050 Hammamet, Tunisia"},
                    {"9fa37086-2740-4be5-b4f5-030b2dd622d5", "Medina Belisaire And Thalasso" ,"Hammamet" , "Bp. 204 8050 Yasmine Hammamet, 8050 Hammamet, Tunisia"},
                    {"bbfc9db7-4fbb-4524-bdaa-5362c0ffb042", "Royal Azur Thalassa" ,"Hammamet" , "AVENUE ASSAD IBN FOURAT HAMMAMET, 8050 Hammamet, Tunisia "}
                });

                migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "RoomId","RoomTypeId","Floor","Number","HotelId","Name","IsAvailable" },
                values: new object[,]
                {
                    {"d3a7a9b9-3c24-49d4-a927-2cc22040eec7",2,4,401,"2b4314f7-5f2a-4ee3-8e39-49860d3e2275","SS401",true},
                    {"f5858551-3eef-487f-94ca-062e413da686",2,2,212,"2b4314f7-5f2a-4ee3-8e39-49860d3e2275","SS212",true},
                    {"c7527815-c32f-4f5c-9118-0431eef93372",1,8,803,"2b4314f7-5f2a-4ee3-8e39-49860d3e2275","DS803",true},
                    {"18298b4a-7dd8-4ee7-bda3-7168ae55799e",3,6,613,"2b4314f7-5f2a-4ee3-8e39-49860d3e2275","DG613",true},
                    {"999e544d-d0da-4c28-a362-2c0a1051a843",4,3,302,"9fa37086-2740-4be5-b4f5-030b2dd622d5","SG302",true}
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"RoomTypes\"");
        }
    }
}
