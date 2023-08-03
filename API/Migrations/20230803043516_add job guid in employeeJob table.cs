using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class addjobguidinemployeeJobtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_interviews_tb_m_jobs_job_guid",
                table: "tb_m_interviews");

            migrationBuilder.DropTable(
                name: "tb_tr_employee_interview");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_interviews_job_guid",
                table: "tb_m_interviews");

            migrationBuilder.DropColumn(
                name: "job_guid",
                table: "tb_m_interviews");

            migrationBuilder.CreateTable(
                name: "tb_tr_employee_job",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    employee_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    interview_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    job_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status_approval = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_employee_job", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_tr_employee_job_tb_m_employees_employee_guid",
                        column: x => x.employee_guid,
                        principalTable: "tb_m_employees",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_tr_employee_job_tb_m_interviews_interview_guid",
                        column: x => x.interview_guid,
                        principalTable: "tb_m_interviews",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_tb_tr_employee_job_tb_m_jobs_job_guid",
                        column: x => x.job_guid,
                        principalTable: "tb_m_jobs",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_employee_job_employee_guid",
                table: "tb_tr_employee_job",
                column: "employee_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_employee_job_interview_guid",
                table: "tb_tr_employee_job",
                column: "interview_guid",
                unique: true,
                filter: "[interview_guid] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_employee_job_job_guid",
                table: "tb_tr_employee_job",
                column: "job_guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_tr_employee_job");

            migrationBuilder.AddColumn<Guid>(
                name: "job_guid",
                table: "tb_m_interviews",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "tb_tr_employee_interview",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    employee_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    interview_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status_approval = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_employee_interview", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_tr_employee_interview_tb_m_employees_employee_guid",
                        column: x => x.employee_guid,
                        principalTable: "tb_m_employees",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_tr_employee_interview_tb_m_interviews_interview_guid",
                        column: x => x.interview_guid,
                        principalTable: "tb_m_interviews",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_interviews_job_guid",
                table: "tb_m_interviews",
                column: "job_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_employee_interview_employee_guid",
                table: "tb_tr_employee_interview",
                column: "employee_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_employee_interview_interview_guid",
                table: "tb_tr_employee_interview",
                column: "interview_guid");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_interviews_tb_m_jobs_job_guid",
                table: "tb_m_interviews",
                column: "job_guid",
                principalTable: "tb_m_jobs",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
