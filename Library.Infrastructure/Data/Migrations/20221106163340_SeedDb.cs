using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Infrastructure.Data.Migrations
{
    public partial class SeedDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Credits", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", 0, "c5ccb707-8731-4a29-b639-f868442f3474", 0, "guest@mail.com", false, false, null, null, null, "AQAAAAEAACcQAAAAEISUntBvRlU7vPwu/vs5n1RnTwpqmAmsXE3LSu4GIcyvygChUV/ZzeSxtzXuprYfWA==", null, false, "2142154a-c74e-45ad-a46e-0037ddb0a5a3", false, "guest@mail.com" },
                    { "dea12856-c198-4129-b3f3-b893d8395082", 0, "85044dc2-834f-4498-b1ef-f39370ea5567", 0, "agent@mail.com", false, false, null, null, null, "AQAAAAEAACcQAAAAEE55qRFgVsyLZfZ33X/Y+Icjt63KjL0amoCzdhbdZe0Stjosce7/lNYeT5VDo3rYCg==", null, false, "f0a58908-e06c-4d7d-8329-8b417bbddac2", false, "agent@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Biography" },
                    { 3, "Children" },
                    { 4, "Crime" },
                    { 5, "Fantasy" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Description", "ImageUrl", "Rating", "Title" },
                values: new object[] { 5, "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", 1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", "https://img.freepik.com/free-psd/book-cover-mock-up-arrangement_23-2148622888.jpg?w=826&t=st=1666106877~exp=1666107477~hmac=5dea3e5634804683bccfebeffdbde98371db37bc2d1a208f074292c862775e1b", 9.5m, "Lorem Ipsum" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
