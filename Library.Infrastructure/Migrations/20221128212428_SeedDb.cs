using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Infrastructure.Migrations
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
                    { "327ec38d-c3eb-4f67-aeaa-8adc1e4d9e84", 0, "1cbc7d6f-6c2c-4d32-8857-2195380dabf1", 0, "administrator@mail.com", false, false, null, "ADMINISTRATOR@MAIL.COM", "ADMINISTRATOR", "AQAAAAEAACcQAAAAEPXcwUGMHTEdWhqCNzibZMWi2QhQpB49P63/umBLi4nN5b8eTXpQ7vFSMIoTnpIsSg==", null, false, "88c82644-b550-4986-be65-4f3045065bed", false, "administrator" },
                    { "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", 0, "a780c3e4-efa6-4495-9560-a7431997f674", 0, "guest@mail.com", false, false, null, "GUEST@MAIL.COM", "GUEST", "AQAAAAEAACcQAAAAEPMeBudj4rU4IHlXmxWqHdhwgNTyg4vEFMQ1O658/y/my3kr/t7+YX62V91NGUX4WA==", null, false, "6b3548cf-956e-43c0-85c2-d738432c6cfa", false, "guest" },
                    { "dea12856-c198-4129-b3f3-b893d8395082", 0, "9759ab65-b9f9-45f0-9f8c-b34ada7916cf", 0, "agent@mail.com", false, false, null, "AGENT@MAIL.COM", "AGENT", "AQAAAAEAACcQAAAAEMGB6rEv6ZN/ElGIFV1ky8AaXICW2CIB0NZgkiUcsvaPq+Ps+zmuRLtPlLO4Ehd8nw==", null, false, "f295fc2e-da73-4164-b176-c62df787b343", false, "agent" }
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
                    { 5, "Fantasy" },
                    { 6, "Fiction" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "PdfUrl", "PublisherId", "Title" },
                values: new object[] { 1, 6, "Simon Singh brings life to an amazing story of puzzles, codes, languages and riddles – revealing the continual pursuit to disguise and uncover, and to work out the secret languages of others. Codes have influenced events throughout history, both in the stories of those who make them and those who break them. ", "crackthecode.jpeg", "CrackTheCode.pdf", "dea12856-c198-4129-b3f3-b893d8395082", "Crack The Code" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "PdfUrl", "PublisherId", "Title" },
                values: new object[] { 2, 5, "It is a story about Harry Potter, an orphan brought up by his aunt and uncle because his parents were killed when he was a baby. Harry is unloved by his uncle and aunt but everything changes when he is invited to join Hogwarts School of Witchcraft and Wizardry and he finds out he's a wizard", "Harry_Potter.jpg", "HarryPotter.pdf", "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", "Harry Potter" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "327ec38d-c3eb-4f67-aeaa-8adc1e4d9e84");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

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
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
