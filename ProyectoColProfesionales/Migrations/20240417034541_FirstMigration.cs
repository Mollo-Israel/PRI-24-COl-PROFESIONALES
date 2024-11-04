using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoColProfesionales.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activity1",
                columns: table => new
                {
                    idActivity = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateActivity = table.Column<DateTime>(type: "datetime2", nullable: false),
                    auditorium = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idThesis = table.Column<int>(type: "int", nullable: false),
                    registerDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: false),
                    idUserRegister = table.Column<int>(type: "int", nullable: false),
                    idUserLastUpdate = table.Column<int>(type: "int", nullable: false),
                    latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    longitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    stateActivity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hasAssistance = table.Column<bool>(type: "bit", nullable: false),
                    hasPayment = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity1", x => x.idActivity);
                });

            migrationBuilder.CreateTable(
                name: "ActivityProfessional",
                columns: table => new
                {
                    idActivityProfessional = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idProfessional = table.Column<int>(type: "int", nullable: false),
                    idActivity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityProfessional", x => x.idActivityProfessional);
                });

            migrationBuilder.CreateTable(
                name: "ActivityVoucher",
                columns: table => new
                {
                    idActivityVoucher = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    voucherType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    voucherFile = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    nameFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idActivity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityVoucher", x => x.idActivityVoucher);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    IdPerson = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Names = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentityNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    IdUserRegister = table.Column<int>(type: "int", nullable: false),
                    IdUserLastUpdate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.IdPerson);
                });

            migrationBuilder.CreateTable(
                name: "Professional",
                columns: table => new
                {
                    idProfessional = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    specialty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idPerson = table.Column<int>(type: "int", nullable: false),
                    registerDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: false),
                    idUserRegister = table.Column<int>(type: "int", nullable: false),
                    idUserLastUpdate = table.Column<int>(type: "int", nullable: false),
                    universidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    carrera = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professional", x => x.idProfessional);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activity1");

            migrationBuilder.DropTable(
                name: "ActivityProfessional");

            migrationBuilder.DropTable(
                name: "ActivityVoucher");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Professional");
        }

    }
}
