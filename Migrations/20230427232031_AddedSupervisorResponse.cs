using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscussionMvcSantiago.Migrations
{
    public partial class AddedSupervisorResponse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Responses",
                table: "ServiceRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Responses",
                table: "Note",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Responses",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "Responses",
                table: "Note");
        }
    }
}
