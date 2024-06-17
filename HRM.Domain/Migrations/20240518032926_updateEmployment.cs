using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRM.Domain.Migrations
{
    /// <inheritdoc />
    public partial class updateEmployment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BENEFIT_PLANS",
                columns: table => new
                {
                    BENEFIT_PLANS_ID = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    PLAN_NAME = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    DEDUCTABLE = table.Column<decimal>(type: "money", nullable: true),
                    PERCENTAGE_COPAY = table.Column<decimal>(type: "numeric(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BENEFIT_PLANS", x => x.BENEFIT_PLANS_ID);
                });

            migrationBuilder.CreateTable(
                name: "PERSONAL",
                columns: table => new
                {
                    PERSONAL_ID = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    CURRENT_FIRST_NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CURRENT_LAST_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CURRENT_MIDDLE_NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BIRTH_DATE = table.Column<DateOnly>(type: "date", nullable: true),
                    SOCIAL_SECURITY_NUMBER = table.Column<decimal>(type: "decimal(18,2)", maxLength: 20, nullable: true),
                    DRIVERS_LICENSE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CURRENT_ADDRESS_1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CURRENT_ADDRESS_2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CURRENT_CITY = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CURRENT_COUNTRY = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CURRENT_ZIP = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    CURRENT_GENDER = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CURRENT_PHONE_NUMBER = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    CURRENT_PERSONAL_EMAIL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CURRENT_MARITAL_STATUS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ETHNICITY = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    SHAREHOLDER_STATUS = table.Column<short>(type: "smallint", nullable: true),
                    BENEFIT_PLAN_ID = table.Column<decimal>(type: "numeric(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERSONAL", x => x.PERSONAL_ID);
                    table.ForeignKey(
                        name: "FK_PERSONAL_BENEFIT_PLANS",
                        column: x => x.BENEFIT_PLAN_ID,
                        principalTable: "BENEFIT_PLANS",
                        principalColumn: "BENEFIT_PLANS_ID");
                });

            migrationBuilder.CreateTable(
                name: "EMPLOYMENT",
                columns: table => new
                {
                    EMPLOYMENT_ID = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    EMPLOYMENT_CODE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EMPLOYMENT_STATUS = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    HIRE_DATE_FOR_WORKING = table.Column<DateOnly>(type: "date", nullable: true),
                    WORKERS_COMP_CODE = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true, comment: "MÃ CÔNG VIỆC"),
                    TERMINATION_DATE = table.Column<DateOnly>(type: "date", nullable: true),
                    REHIRE_DATE_FOR_WORKING = table.Column<DateOnly>(type: "date", nullable: true),
                    LAST_REVIEW_DATE = table.Column<DateOnly>(type: "date", nullable: true),
                    NUMBER_DAYS_REQUIREMENT_OF_WORKING_PER_MONTH = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    PERSONAL_ID = table.Column<decimal>(type: "numeric(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPLOYMENT", x => x.EMPLOYMENT_ID);
                    table.ForeignKey(
                        name: "FK_EMPLOYMENT_PERSONAL",
                        column: x => x.PERSONAL_ID,
                        principalTable: "PERSONAL",
                        principalColumn: "PERSONAL_ID");
                });

            migrationBuilder.CreateTable(
                name: "EMPLOYMENT_WORKING_TIME",
                columns: table => new
                {
                    EMPLOYMENT_WORKING_TIME_ID = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    EMPLOYMENT_ID = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    YEAR_WORKING = table.Column<DateOnly>(type: "date", nullable: true),
                    MONTH_WORKING = table.Column<decimal>(type: "numeric(2,0)", nullable: true),
                    NUMBER_DAYS_ACTUAL_OF_WORKING_PER_MONTH = table.Column<decimal>(type: "numeric(2,0)", nullable: true),
                    TOTAL_NUMBER_VACATION_WORKING_DAYS_PER_MONTH = table.Column<decimal>(type: "numeric(2,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPLOYMENT_WORKING_TIME", x => x.EMPLOYMENT_WORKING_TIME_ID);
                    table.ForeignKey(
                        name: "FK_EMPLOYMENT_WORKING_TIME_EMPLOYMENT",
                        column: x => x.EMPLOYMENT_ID,
                        principalTable: "EMPLOYMENT",
                        principalColumn: "EMPLOYMENT_ID");
                });

            migrationBuilder.CreateTable(
                name: "JOB_HISTORY",
                columns: table => new
                {
                    JOB_HISTORY_ID = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    EMPLOYMENT_ID = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    DEPARTMENT = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DIVISION = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FROM_DATE = table.Column<DateOnly>(type: "date", nullable: true),
                    THRU_DATE = table.Column<DateOnly>(type: "date", nullable: true),
                    JOB_TITLE = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SUPERVISOR = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LOCATION = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TYPE_OF_WORK = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JOB_HISTORY", x => x.JOB_HISTORY_ID);
                    table.ForeignKey(
                        name: "FK_JOB_HISTORY_EMPLOYMENT",
                        column: x => x.EMPLOYMENT_ID,
                        principalTable: "EMPLOYMENT",
                        principalColumn: "EMPLOYMENT_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EMPLOYMENT_PERSONAL_ID",
                table: "EMPLOYMENT",
                column: "PERSONAL_ID",
                unique: true,
                filter: "[PERSONAL_ID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EMPLOYMENT_WORKING_TIME_EMPLOYMENT_ID",
                table: "EMPLOYMENT_WORKING_TIME",
                column: "EMPLOYMENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_JOB_HISTORY_EMPLOYMENT_ID",
                table: "JOB_HISTORY",
                column: "EMPLOYMENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PERSONAL_BENEFIT_PLAN_ID",
                table: "PERSONAL",
                column: "BENEFIT_PLAN_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EMPLOYMENT_WORKING_TIME");

            migrationBuilder.DropTable(
                name: "JOB_HISTORY");

            migrationBuilder.DropTable(
                name: "EMPLOYMENT");

            migrationBuilder.DropTable(
                name: "PERSONAL");

            migrationBuilder.DropTable(
                name: "BENEFIT_PLANS");
        }
    }
}
