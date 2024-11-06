using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoColProfesionales.Migrations.DBColProfessional
{
    public partial class AddNotification2Table4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Person",
            //    columns: table => new
            //    {
            //        idPerson = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        names = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
            //        lastname = table.Column<string>(type: "varchar(35)", unicode: false, maxLength: 35, nullable: false),
            //        secondLastName = table.Column<string>(type: "varchar(35)", unicode: false, maxLength: 35, nullable: true),
            //        identityNumber = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
            //        phoneNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
            //        email = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: true),
            //        registerDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
            //        lastUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
            //        status = table.Column<byte>(type: "tinyint", nullable: false, defaultValueSql: "((1))"),
            //        idUserRegister = table.Column<int>(type: "int", nullable: true),
            //        idUserLastUpdate = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Person", x => x.idPerson);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Thesis",
            //    columns: table => new
            //    {
            //        idThesis = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
            //        description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
            //        student = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
            //        career = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
            //        registerDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
            //        lastUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
            //        status = table.Column<byte>(type: "tinyint", nullable: false, defaultValueSql: "((1))"),
            //        idUserRegister = table.Column<int>(type: "int", nullable: true),
            //        idUserLastUpdate = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Thesis", x => x.idThesis);
            //    });

            migrationBuilder.CreateTable(
                name: "Notifications2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Date1 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date2 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date3 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    IdPerson = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification2_Person",
                        column: x => x.IdPerson,
                        principalTable: "Person",
                        principalColumn: "idPerson");
                });

            //migrationBuilder.CreateTable(
            //    name: "Professional",
            //    columns: table => new
            //    {
            //        idProfessional = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        specialty = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //        code = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //        idPerson = table.Column<int>(type: "int", nullable: false),
            //        registerDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
            //        lastUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
            //        status = table.Column<byte>(type: "tinyint", nullable: false, defaultValueSql: "((1))"),
            //        idUserRegister = table.Column<int>(type: "int", nullable: true),
            //        idUserLastUpdate = table.Column<int>(type: "int", nullable: true),
            //        university = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
            //        career = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Professional", x => x.idProfessional);
            //        table.ForeignKey(
            //            name: "FK_Professional_Person1",
            //            column: x => x.idPerson,
            //            principalTable: "Person",
            //            principalColumn: "idPerson");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "User",
            //    columns: table => new
            //    {
            //        idUser = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        userName = table.Column<string>(type: "varchar(35)", unicode: false, maxLength: 35, nullable: false),
            //        password = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
            //        role = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //        idPerson = table.Column<int>(type: "int", nullable: false),
            //        registerDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
            //        lastUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
            //        status = table.Column<byte>(type: "tinyint", nullable: false, defaultValueSql: "((1))"),
            //        idUserRegister = table.Column<int>(type: "int", nullable: true),
            //        idUserLastUpdate = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_User", x => x.idUser);
            //        table.ForeignKey(
            //            name: "FK_User_Person",
            //            column: x => x.idPerson,
            //            principalTable: "Person",
            //            principalColumn: "idPerson");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Activity",
            //    columns: table => new
            //    {
            //        idActivity = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        description = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //        dateActivity = table.Column<DateTime>(type: "datetime", nullable: false),
            //        auditorium = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //        place = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //        idThesis = table.Column<int>(type: "int", nullable: false),
            //        registerDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
            //        lastUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
            //        status = table.Column<byte>(type: "tinyint", nullable: false, defaultValueSql: "((1))"),
            //        idUserRegister = table.Column<int>(type: "int", nullable: true),
            //        idUserLastUpdate = table.Column<int>(type: "int", nullable: true),
            //        latitude = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
            //        longitude = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
            //        stateActivity = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
            //        hasAssistance = table.Column<bool>(type: "bit", nullable: true),
            //        hasPayment = table.Column<bool>(type: "bit", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Activity", x => x.idActivity);
            //        table.ForeignKey(
            //            name: "FK_Activity_Thesis",
            //            column: x => x.idThesis,
            //            principalTable: "Thesis",
            //            principalColumn: "idThesis");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ThesisFile",
            //    columns: table => new
            //    {
            //        idThesisFile = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ThesisType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //        DataFile = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
            //        nameFile = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
            //        idThesis = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ThesisFile", x => x.idThesisFile);
            //        table.ForeignKey(
            //            name: "FK_ThesisFile_Thesis",
            //            column: x => x.idThesis,
            //            principalTable: "Thesis",
            //            principalColumn: "idThesis");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ActivityProfessional",
            //    columns: table => new
            //    {
            //        idActivityProfessional = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        idProfessional = table.Column<int>(type: "int", nullable: false),
            //        idActivity = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ActivityProfessional", x => x.idActivityProfessional);
            //        table.ForeignKey(
            //            name: "FK_ActivityProfessional_Activity",
            //            column: x => x.idActivity,
            //            principalTable: "Activity",
            //            principalColumn: "idActivity");
            //        table.ForeignKey(
            //            name: "FK_ActivityProfessional_Professional",
            //            column: x => x.idProfessional,
            //            principalTable: "Professional",
            //            principalColumn: "idProfessional");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ActivityVoucher",
            //    columns: table => new
            //    {
            //        idActivityVoucher = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        voucherType = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
            //        voucherFile = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
            //        nameFile = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
            //        idActivity = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ActivityVoucher", x => x.idActivityVoucher);
            //        table.ForeignKey(
            //            name: "FK_ActivityVoucher_Activity",
            //            column: x => x.idActivity,
            //            principalTable: "Activity",
            //            principalColumn: "idActivity");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Letter",
            //    columns: table => new
            //    {
            //        idLetter = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        idProfessional = table.Column<int>(type: "int", nullable: true),
            //        format = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
            //        idActivity = table.Column<int>(type: "int", nullable: true),
            //        registerDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
            //        lastUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
            //        status = table.Column<byte>(type: "tinyint", nullable: false, defaultValueSql: "((1))"),
            //        idUserRegister = table.Column<int>(type: "int", nullable: true),
            //        idUserLastUpdate = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Letter", x => x.idLetter);
            //        table.ForeignKey(
            //            name: "FK_Letter_Activity",
            //            column: x => x.idActivity,
            //            principalTable: "Activity",
            //            principalColumn: "idActivity");
            //        table.ForeignKey(
            //            name: "FK_Letter_Professional1",
            //            column: x => x.idProfessional,
            //            principalTable: "Professional",
            //            principalColumn: "idProfessional");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Notification",
            //    columns: table => new
            //    {
            //        idNotification = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        message = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
            //        dateTime = table.Column<DateTime>(type: "datetime", nullable: true),
            //        idProfessional = table.Column<int>(type: "int", nullable: true),
            //        idLetter = table.Column<int>(type: "int", nullable: true),
            //        registerDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
            //        lastUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
            //        status = table.Column<byte>(type: "tinyint", nullable: false, defaultValueSql: "((1))"),
            //        idUserRegister = table.Column<int>(type: "int", nullable: true),
            //        idUserLastUpdate = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Notification", x => x.idNotification);
            //        table.ForeignKey(
            //            name: "FK_Notification_Letter",
            //            column: x => x.idLetter,
            //            principalTable: "Letter",
            //            principalColumn: "idLetter");
            //        table.ForeignKey(
            //            name: "FK_Notification_Professional1",
            //            column: x => x.idProfessional,
            //            principalTable: "Professional",
            //            principalColumn: "idProfessional");
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Activity_idThesis",
            //    table: "Activity",
            //    column: "idThesis");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ActivityProfessional_idActivity",
            //    table: "ActivityProfessional",
            //    column: "idActivity");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ActivityProfessional_idProfessional",
            //    table: "ActivityProfessional",
            //    column: "idProfessional");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ActivityVoucher_idActivity",
            //    table: "ActivityVoucher",
            //    column: "idActivity");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Letter_idActivity",
            //    table: "Letter",
            //    column: "idActivity");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Letter_idProfessional",
            //    table: "Letter",
            //    column: "idProfessional");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Notification_idLetter",
            //    table: "Notification",
            //    column: "idLetter");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Notification_idProfessional",
            //    table: "Notification",
            //    column: "idProfessional");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications2_IdPerson",
                table: "Notifications2",
                column: "IdPerson");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Professional_idPerson",
            //    table: "Professional",
            //    column: "idPerson");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ThesisFile_idThesis",
            //    table: "ThesisFile",
            //    column: "idThesis");

            //migrationBuilder.CreateIndex(
            //    name: "IX_User_idPerson",
            //    table: "User",
            //    column: "idPerson");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "ActivityProfessional");

            //migrationBuilder.DropTable(
            //    name: "ActivityVoucher");

            //migrationBuilder.DropTable(
            //    name: "Notification");

            migrationBuilder.DropTable(
                name: "Notifications2");

            //migrationBuilder.DropTable(
            //    name: "ThesisFile");

            //migrationBuilder.DropTable(
            //    name: "User");

            //migrationBuilder.DropTable(
            //    name: "Letter");

            //migrationBuilder.DropTable(
            //    name: "Activity");

            //migrationBuilder.DropTable(
            //    name: "Professional");

            //migrationBuilder.DropTable(
            //    name: "Thesis");

            //migrationBuilder.DropTable(
            //    name: "Person");
        }
    }
}
