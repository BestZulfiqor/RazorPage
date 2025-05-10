using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changedisResevedtofromtoReservationdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReserved",
                table: "Tables");

            migrationBuilder.RenameColumn(
                name: "ReservationDate",
                table: "Reservations",
                newName: "ReservationDateStart");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ReservationDateEnd",
                table: "Reservations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationDateEnd",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "ReservationDateStart",
                table: "Reservations",
                newName: "ReservationDate");

            migrationBuilder.AddColumn<bool>(
                name: "IsReserved",
                table: "Tables",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
