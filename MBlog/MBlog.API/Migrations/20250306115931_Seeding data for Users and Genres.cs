using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MBlog.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdataforUsersandGenres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1543228b-9f94-4b11-b7cd-5b9990bec802"), "Programming" },
                    { new Guid("2f5f840e-ebcb-422b-805b-bc908b66269b"), "Travel" },
                    { new Guid("3e9e946f-e961-4f26-bd16-6ae3c7d8b799"), "Film" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("38ff1a54-3661-4923-90b2-3283b57b0613"), "Anna", "Boyko" },
                    { new Guid("3f6f6389-8509-4987-b109-e1b895ad686c"), "Alina", "Fedak" },
                    { new Guid("a05d6a71-c602-4a50-8535-552c26daab92"), "Andrew", "Miller" },
                    { new Guid("cb37f95d-aee6-48c5-8624-d730c2806c34"), "John", "Smith" },
                    { new Guid("f832050f-5325-468a-bd97-dc86d64d5495"), "Olga", "Tokar" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("1543228b-9f94-4b11-b7cd-5b9990bec802"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("2f5f840e-ebcb-422b-805b-bc908b66269b"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("3e9e946f-e961-4f26-bd16-6ae3c7d8b799"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("38ff1a54-3661-4923-90b2-3283b57b0613"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3f6f6389-8509-4987-b109-e1b895ad686c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a05d6a71-c602-4a50-8535-552c26daab92"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cb37f95d-aee6-48c5-8624-d730c2806c34"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f832050f-5325-468a-bd97-dc86d64d5495"));
        }
    }
}
