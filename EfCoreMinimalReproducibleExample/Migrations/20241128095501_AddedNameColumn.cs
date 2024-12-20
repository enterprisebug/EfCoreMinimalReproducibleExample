﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfCoreMinimalReproducibleExample.Migrations
{
    /// <inheritdoc />
    public partial class AddedNameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MyEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "MyEntities");
        }
    }
}
