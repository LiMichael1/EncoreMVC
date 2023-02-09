using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EncoreSample.Data.Migrations
{
    public partial class AddLikesandDislikesTallyToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Vote",
                table: "Audios",
                newName: "NumberOfLikes");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfDislikes",
                table: "Audios",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfDislikes",
                table: "Audios");

            migrationBuilder.RenameColumn(
                name: "NumberOfLikes",
                table: "Audios",
                newName: "Vote");
        }
    }
}
