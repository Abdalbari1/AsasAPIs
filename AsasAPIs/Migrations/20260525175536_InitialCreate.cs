using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsasAPIs.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Com_id = table.Column<int>(type: "int", nullable: false),
                    Emp_id = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Date_of_end = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Company__D85724E45D221B33", x => x.Com_id);
                });

            migrationBuilder.CreateTable(
                name: "Dep",
                columns: table => new
                {
                    Com_id = table.Column<int>(type: "int", nullable: false),
                    Dep_id = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Emp_id = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Dep_N = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Dep__9895A0F1FF9E6E83", x => new { x.Com_id, x.Dep_id });
                });

            migrationBuilder.CreateTable(
                name: "issue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mass = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_issue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AddE",
                columns: table => new
                {
                    Com_id = table.Column<int>(type: "int", nullable: false),
                    Emp_id = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Dep_id = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Role = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AddE__FA34C6393A67D366", x => new { x.Com_id, x.Emp_id });
                    table.ForeignKey(
                        name: "FK_Dep_to_AddE",
                        columns: x => new { x.Com_id, x.Emp_id },
                        principalTable: "Dep",
                        principalColumns: new[] { "Com_id", "Dep_id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Acc",
                columns: table => new
                {
                    Com_id = table.Column<int>(type: "int", nullable: false),
                    Emp_id = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Pass = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    IV = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Iterations = table.Column<int>(type: "int", nullable: false),
                    Salt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PhoneN = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Acc__FA34C6392624548B", x => new { x.Com_id, x.Emp_id });
                    table.ForeignKey(
                        name: "A",
                        columns: x => new { x.Com_id, x.Emp_id },
                        principalTable: "AddE",
                        principalColumns: new[] { "Com_id", "Emp_id" },
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Acc");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "issue");

            migrationBuilder.DropTable(
                name: "AddE");

            migrationBuilder.DropTable(
                name: "Dep");
        }
    }
}
