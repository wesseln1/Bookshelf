using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShelf.Migrations
{
    public partial class comments_on_books : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "df563c8c-702c-4a67-b1e3-a5e26b2a7ac9", "AQAAAAEAACcQAAAAEFh90ycgfIvcoBuZaLr4KOJ6c8UVFdnfxJ6LhToAjSDgr1F7D1xI+4u1khn36jYAUQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4b08cf5f-541e-4c30-9b6e-8c08d4994891", "AQAAAAEAACcQAAAAEPvt5DpDC6GUw2IV9zs2y+0qpPnQIs+G3fwlX0iHrjNzEHRUKCDx/jHDeU1O4L9ItQ==" });
        }
    }
}
