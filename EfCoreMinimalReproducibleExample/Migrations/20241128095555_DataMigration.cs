using EfCoreMinimalReproducibleExample.Models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfCoreMinimalReproducibleExample.Migrations
{
    /// <inheritdoc />
    public partial class DataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"UPDATE dbo.MyEntities SET Name = 'New Name'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
