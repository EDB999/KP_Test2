using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KP_Test2.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "driver_idlink_fkey",
                table: "driver");

            migrationBuilder.DropIndex(
                name: "IX_driver_idlink",
                table: "driver");

            migrationBuilder.DropColumn(
                name: "idlink",
                table: "driver");

            migrationBuilder.AlterColumn<string>(
                name: "route",
                table: "userorder",
                type: "text",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<int>(
                name: "iddriver",
                table: "userorder",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "paysize",
                table: "userorder",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_userorder_iddriver",
                table: "userorder",
                column: "iddriver");

            migrationBuilder.AddForeignKey(
                name: "userorder_iddriver_fkey",
                table: "userorder",
                column: "iddriver",
                principalTable: "driver",
                principalColumn: "iddriver");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "userorder_iddriver_fkey",
                table: "userorder");

            migrationBuilder.DropIndex(
                name: "IX_userorder_iddriver",
                table: "userorder");

            migrationBuilder.DropColumn(
                name: "iddriver",
                table: "userorder");

            migrationBuilder.DropColumn(
                name: "paysize",
                table: "userorder");

            migrationBuilder.AlterColumn<float>(
                name: "route",
                table: "userorder",
                type: "real",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "idlink",
                table: "driver",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_driver_idlink",
                table: "driver",
                column: "idlink");

            migrationBuilder.AddForeignKey(
                name: "driver_idlink_fkey",
                table: "driver",
                column: "idlink",
                principalTable: "userorder",
                principalColumn: "iduserorder");
        }
    }
}
