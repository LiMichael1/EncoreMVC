using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EncoreSample.Data.Migrations
{
    public partial class removeSongCover : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SongCoverLink",
                table: "Audios");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SongCoverLink",
                table: "Audios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
