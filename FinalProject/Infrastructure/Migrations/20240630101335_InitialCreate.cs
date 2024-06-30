using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    CoachId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    TrainingGroupId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Members_Groups_TrainingGroupId",
                        column: x => x.TrainingGroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Trainings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PreNotes = table.Column<string>(type: "TEXT", nullable: true),
                    PostNotes = table.Column<string>(type: "TEXT", nullable: true),
                    GroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    TrainingStart = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TrainingEnd = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainings_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Name", "Password", "TrainingGroupId", "Type" },
                values: new object[,]
                {
                    { 1, "Właściciel", "haslo123", null, 2 },
                    { 2, "Trener", "haslo123", null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CoachId", "Description", "Name", "Type" },
                values: new object[] { 1, 2, "Zawiera samych słabiaków z naszego klubu", "Grupa słabiaków", 0 });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Name", "Password", "TrainingGroupId", "Type" },
                values: new object[,]
                {
                    { 3, "Zawodnik 1", "haslo123", 1, 0 },
                    { 4, "Zawodnik 2", "haslo123", 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "Trainings",
                columns: new[] { "Id", "GroupId", "PostNotes", "PreNotes", "TrainingEnd", "TrainingStart" },
                values: new object[,]
                {
                    { 1, 1, "Po wyjaśnieniu jak biegać nadal nic nie ogarniają", "Trzeba im wyjaśnic na treningu jak się biega bo nie umieją", new DateTime(2024, 6, 30, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 30, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, null, "Podjąć kolejną próbę nauczenia ich biegać", new DateTime(2024, 7, 30, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 30, 10, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CoachId",
                table: "Groups",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_TrainingGroupId",
                table: "Members",
                column: "TrainingGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_GroupId",
                table: "Trainings",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Members_CoachId",
                table: "Groups",
                column: "CoachId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Members_CoachId",
                table: "Groups");

            migrationBuilder.DropTable(
                name: "Trainings");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
