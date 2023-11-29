using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KP_Test2.Migrations
{
    /// <inheritdoc />
    public partial class init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "car",
                columns: table => new
                {
                    idcar = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    model = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    numbers = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    isautopark = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("car_pkey", x => x.idcar);
                });

            migrationBuilder.CreateTable(
                name: "usertaxi",
                columns: table => new
                {
                    iduser = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    surname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    login = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    contact = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    card = table.Column<long>(type: "bigint", nullable: true),
                    dateregistration = table.Column<DateOnly>(type: "date", nullable: false),
                    roletype = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("usertaxi_pkey", x => x.iduser);
                });

            migrationBuilder.CreateTable(
                name: "driver",
                columns: table => new
                {
                    iddriver = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rating = table.Column<double>(type: "double precision", nullable: true),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    iswork = table.Column<bool>(type: "boolean", nullable: false),
                    plane = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    idcar = table.Column<int>(type: "integer", nullable: true),
                    license = table.Column<int>(type: "integer", nullable: false),
                    iduser = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("driver_pkey", x => x.iddriver);
                    table.ForeignKey(
                        name: "driver_idcar_fkey",
                        column: x => x.idcar,
                        principalTable: "car",
                        principalColumn: "idcar");
                    table.ForeignKey(
                        name: "driver_iduser_fkey",
                        column: x => x.iduser,
                        principalTable: "usertaxi",
                        principalColumn: "iduser");
                });

            migrationBuilder.CreateTable(
                name: "passenger",
                columns: table => new
                {
                    idpassenger = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rating = table.Column<double>(type: "double precision", nullable: true),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    addresshome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    iduser = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("passenger_pkey", x => x.idpassenger);
                    table.ForeignKey(
                        name: "passenger_iduser_fkey",
                        column: x => x.iduser,
                        principalTable: "usertaxi",
                        principalColumn: "iduser");
                });

            migrationBuilder.CreateTable(
                name: "historyorder",
                columns: table => new
                {
                    idorder = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idpassenger = table.Column<int>(type: "integer", nullable: false),
                    iddriver = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<int>(type: "integer", nullable: false),
                    routestart = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    routeend = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    timestart = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    timeend = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("historyorder_pkey", x => x.idorder);
                    table.ForeignKey(
                        name: "historyorder_iddriver_fkey",
                        column: x => x.iddriver,
                        principalTable: "driver",
                        principalColumn: "iddriver");
                    table.ForeignKey(
                        name: "historyorder_idpassenger_fkey",
                        column: x => x.idpassenger,
                        principalTable: "passenger",
                        principalColumn: "idpassenger");
                });

            migrationBuilder.CreateIndex(
                name: "IX_driver_idcar",
                table: "driver",
                column: "idcar");

            migrationBuilder.CreateIndex(
                name: "IX_driver_iduser",
                table: "driver",
                column: "iduser");

            migrationBuilder.CreateIndex(
                name: "IX_historyorder_iddriver",
                table: "historyorder",
                column: "iddriver");

            migrationBuilder.CreateIndex(
                name: "IX_historyorder_idpassenger",
                table: "historyorder",
                column: "idpassenger");

            migrationBuilder.CreateIndex(
                name: "IX_passenger_iduser",
                table: "passenger",
                column: "iduser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "historyorder");

            migrationBuilder.DropTable(
                name: "driver");

            migrationBuilder.DropTable(
                name: "passenger");

            migrationBuilder.DropTable(
                name: "car");

            migrationBuilder.DropTable(
                name: "usertaxi");
        }
    }
}
