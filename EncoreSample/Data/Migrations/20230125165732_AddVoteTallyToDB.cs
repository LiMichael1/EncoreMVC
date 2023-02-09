using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EncoreSample.Data.Migrations
{
    public partial class AddVoteTallyToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Vote",
                table: "Audios",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vote",
                table: "Audios");
        }
    }
}
