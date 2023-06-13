﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscussionMvcSantiago.Migrations
{
    public partial class FixedUnservicedRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Responses",
                table: "ServiceRequest");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NoteCreated",
                table: "Note",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Responses",
                table: "ServiceRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NoteCreated",
                table: "Note",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
