using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class Seedingdatafordifficultiesandregions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("39a9b70f-d799-4d20-a33c-65230e60ac28"), "Easy" },
                    { new Guid("3b60291b-1546-4b8d-afab-a951ae5d4829"), "Medium" },
                    { new Guid("d249d7cd-9b5e-4bd3-b517-a16f8595a64e"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("14ceba71-4b51-4777-9b17-46602cf66153"), "BOP", "Bay Of Plenty", "https://th.bing.com/th/id/R.4a2c082592782100a50e5c4b13aaedb8?rik=rNZjmrDwRoCbNg&pid=ImgRaw&r=0" },
                    { new Guid("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"), "NTL", "Northland", "https://th.bing.com/th/id/R.4a2c082592782100a50e5c4b13aaedb8?rik=rNZjmrDwRoCbNg&pid=ImgRaw&r=0" },
                    { new Guid("906cb139-415a-4bbb-a174-1a1faf9fb1f6"), "NSN", "Nelson", "https://th.bing.com/th/id/R.4a2c082592782100a50e5c4b13aaedb8?rik=rNZjmrDwRoCbNg&pid=ImgRaw&r=0" },
                    { new Guid("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"), "WGN", "Wellington", "https://th.bing.com/th/id/R.4a2c082592782100a50e5c4b13aaedb8?rik=rNZjmrDwRoCbNg&pid=ImgRaw&r=0" },
                    { new Guid("f077a22e-4248-4bf6-b564-c7cf4e250263"), "STL", "Southland", "https://th.bing.com/th/id/R.4a2c082592782100a50e5c4b13aaedb8?rik=rNZjmrDwRoCbNg&pid=ImgRaw&r=0" },
                    { new Guid("f7248fc3-2585-4efb-8d1d-1c555f4087f6"), "AKL", "Auckland", "https://th.bing.com/th/id/R.4a2c082592782100a50e5c4b13aaedb8?rik=rNZjmrDwRoCbNg&pid=ImgRaw&r=0" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("39a9b70f-d799-4d20-a33c-65230e60ac28"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("3b60291b-1546-4b8d-afab-a951ae5d4829"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("d249d7cd-9b5e-4bd3-b517-a16f8595a64e"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("14ceba71-4b51-4777-9b17-46602cf66153"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("906cb139-415a-4bbb-a174-1a1faf9fb1f6"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f077a22e-4248-4bf6-b564-c7cf4e250263"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f7248fc3-2585-4efb-8d1d-1c555f4087f6"));
        }
    }
}
