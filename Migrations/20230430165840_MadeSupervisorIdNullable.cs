using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscussionMvcSantiago.Migrations
{
    public partial class MadeSupervisorIdNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_AspNetUsers_MechanicId",
                table: "Note");

            migrationBuilder.DropForeignKey(
                name: "FK_Note_AspNetUsers_SupervisorId",
                table: "Note");

            migrationBuilder.AlterColumn<string>(
                name: "SupervisorId",
                table: "Note",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "MechanicId",
                table: "Note",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_AspNetUsers_MechanicId",
                table: "Note",
                column: "MechanicId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_AspNetUsers_SupervisorId",
                table: "Note",
                column: "SupervisorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_AspNetUsers_MechanicId",
                table: "Note");

            migrationBuilder.DropForeignKey(
                name: "FK_Note_AspNetUsers_SupervisorId",
                table: "Note");

            migrationBuilder.AlterColumn<string>(
                name: "SupervisorId",
                table: "Note",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MechanicId",
                table: "Note",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Note_AspNetUsers_MechanicId",
                table: "Note",
                column: "MechanicId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Note_AspNetUsers_SupervisorId",
                table: "Note",
                column: "SupervisorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
