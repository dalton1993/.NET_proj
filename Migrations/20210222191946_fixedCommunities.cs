using Microsoft.EntityFrameworkCore.Migrations;

namespace CS_React_proj.Migrations
{
    public partial class fixedCommunities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Relationships");

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "UserCommunities",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                table: "UserCommunities");

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "Relationships",
                type: "TEXT",
                nullable: true);
        }
    }
}
