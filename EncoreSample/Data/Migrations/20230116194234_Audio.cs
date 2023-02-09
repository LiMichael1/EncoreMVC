using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EncoreSample.Data.Migrations
{
    public partial class Audio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AudioLink",
                table: "Audios",
                newName: "AudioPath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AudioPath",
                table: "Audios",
                newName: "AudioLink");
        }
    }
}
