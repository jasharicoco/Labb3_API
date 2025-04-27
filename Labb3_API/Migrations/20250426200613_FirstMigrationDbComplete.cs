using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Labb3_API.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigrationDbComplete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Interests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterestName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InterestLink",
                columns: table => new
                {
                    InterestsId = table.Column<int>(type: "int", nullable: false),
                    LinksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestLink", x => new { x.InterestsId, x.LinksId });
                    table.ForeignKey(
                        name: "FK_InterestLink_Interests_InterestsId",
                        column: x => x.InterestsId,
                        principalTable: "Interests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterestLink_Links_LinksId",
                        column: x => x.LinksId,
                        principalTable: "Links",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InterestPerson",
                columns: table => new
                {
                    InterestsId = table.Column<int>(type: "int", nullable: false),
                    PersonsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestPerson", x => new { x.InterestsId, x.PersonsId });
                    table.ForeignKey(
                        name: "FK_InterestPerson_Interests_InterestsId",
                        column: x => x.InterestsId,
                        principalTable: "Interests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterestPerson_Persons_PersonsId",
                        column: x => x.PersonsId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LinkPerson",
                columns: table => new
                {
                    LinksId = table.Column<int>(type: "int", nullable: false),
                    PersonsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkPerson", x => new { x.LinksId, x.PersonsId });
                    table.ForeignKey(
                        name: "FK_LinkPerson_Links_LinksId",
                        column: x => x.LinksId,
                        principalTable: "Links",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LinkPerson_Persons_PersonsId",
                        column: x => x.PersonsId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Interests",
                columns: new[] { "Id", "Description", "InterestName" },
                values: new object[,]
                {
                    { 1, "Cykla för att uppnå själsligt välmående.", "Cykling" },
                    { 2, "En utmaning som enbart de allra djärvaste av människor vågar sig på.", "Bergsklättring" },
                    { 3, "För att det är kul.", "Resa" },
                    { 4, "Konsten att återhämta sig.", "Vila" },
                    { 5, "Bygg dina egna applikationer och hemsidor med mera.", "Programmering" },
                    { 6, "Kasta bollen och spring för att vinna.", "Fotboll" },
                    { 7, "Kreativitet i köket!", "Matlagning" },
                    { 8, "För att förstå världen omkring oss.", "Fysik" },
                    { 9, "Kreativa uttryck för själens bästa.", "Konst" },
                    { 10, "Filmer för att fånga fantasin.", "Film" }
                });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "Id", "Description", "Url" },
                values: new object[,]
                {
                    { 1, "Information om cykling. Hur man gör osv.", "https://www.cykling.se" },
                    { 2, "Information om turer runtom i landet.", "https://www.cykelturer.se" },
                    { 3, "Information om bergsklättring.", "https://www.bergsklattring.se" },
                    { 4, "Information om alla faror kopplade till bergsklättring.", "https://www.klattraberg.se" },
                    { 5, "Hitta dina resor här.", "https://www.ving.se" },
                    { 6, "En till sida att hitta sina resor på.", "https://www.apollo.se" },
                    { 7, "Sök billiga flyg.", "https://www.flygresor.se" },
                    { 8, "Lär dig vila.", "https://www.meditation.se" },
                    { 9, "Allt om fotboll.", "https://www.fotboll.se" },
                    { 10, "Bästa recepten online.", "https://www.matlagning.se" },
                    { 11, "Lär dig fysik genom roliga experiment.", "https://www.astro.se" },
                    { 12, "Se konst från hela världen.", "https://www.konst.se" },
                    { 13, "Streama filmer och tv-serier.", "https://www.film.se" },
                    { 14, "Lägg upp dina projekt här.", "https://www.github.com" },
                    { 15, "Lär dig mer om programmering.", "https://www.stackoverflow.com" }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "alexander@ek.se", "Alexander", "Ek", "070-1234567" },
                    { 2, "serhan@gyuler.uk", "Serhan", "Gyuler", "070-7654321" },
                    { 3, "petter@bostrom.se", "Petter", "Boström", "070-1122334" },
                    { 4, "leon@jashari.se", "Leon", "Jashari", "070-5566778" },
                    { 5, "joel@mako.se", "Joel", "Mako", "070-9988776" }
                });

            migrationBuilder.InsertData(
                table: "InterestLink",
                columns: new[] { "InterestsId", "LinksId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 3, 5 },
                    { 3, 6 },
                    { 4, 8 },
                    { 5, 14 },
                    { 5, 15 },
                    { 6, 9 },
                    { 7, 10 },
                    { 8, 11 },
                    { 9, 12 },
                    { 10, 13 }
                });

            migrationBuilder.InsertData(
                table: "InterestPerson",
                columns: new[] { "InterestsId", "PersonsId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 2, 4 },
                    { 3, 3 },
                    { 5, 4 },
                    { 6, 5 },
                    { 7, 5 },
                    { 8, 5 }
                });

            migrationBuilder.InsertData(
                table: "LinkPerson",
                columns: new[] { "LinksId", "PersonsId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 2, 3 },
                    { 3, 4 },
                    { 4, 5 },
                    { 5, 3 },
                    { 6, 4 },
                    { 7, 5 },
                    { 8, 5 },
                    { 14, 4 },
                    { 15, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InterestLink_LinksId",
                table: "InterestLink",
                column: "LinksId");

            migrationBuilder.CreateIndex(
                name: "IX_InterestPerson_PersonsId",
                table: "InterestPerson",
                column: "PersonsId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkPerson_PersonsId",
                table: "LinkPerson",
                column: "PersonsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterestLink");

            migrationBuilder.DropTable(
                name: "InterestPerson");

            migrationBuilder.DropTable(
                name: "LinkPerson");

            migrationBuilder.DropTable(
                name: "Interests");

            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
