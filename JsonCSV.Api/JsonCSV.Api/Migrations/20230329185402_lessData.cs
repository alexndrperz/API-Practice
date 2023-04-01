﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JsonCSV.Api.Migrations
{
    public partial class lessData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "pointOfInterests",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "pointOfInterests",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "pointOfInterests",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "pointOfInterests",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "pointOfInterests",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "pointOfInterests",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "The one with that big park.", "New York City" });

            migrationBuilder.InsertData(
                table: "pointOfInterests",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 3, 2, "A Gothic style cathedral, conceived by architects Jan and Pieter Appelmans.", "Cathedral" });

            migrationBuilder.InsertData(
                table: "pointOfInterests",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 4, 2, "The the finest example of railway architecture in Belgium.", "Antwerp Central Station" });

            migrationBuilder.InsertData(
                table: "pointOfInterests",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 5, 3, "A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave Eiffel.", "Eiffel Tower" });

            migrationBuilder.InsertData(
                table: "pointOfInterests",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 6, 3, "The world's largest museum.", "The Louvre" });

            migrationBuilder.InsertData(
                table: "pointOfInterests",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 1, 1, "The most visited urban park in the United States.", "Central Park" });

            migrationBuilder.InsertData(
                table: "pointOfInterests",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 2, 1, "A 102-story skyscraper located in Midtown Manhattan.", "Empire State Building" });
        }
    }
}
