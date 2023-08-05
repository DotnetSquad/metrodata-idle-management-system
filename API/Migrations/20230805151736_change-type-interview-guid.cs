using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class changetypeinterviewguid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_employee_job_tb_m_interviews_interview_guid",
                table: "tb_tr_employee_job");

            migrationBuilder.DropIndex(
                name: "IX_tb_tr_employee_job_interview_guid",
                table: "tb_tr_employee_job");

            migrationBuilder.AlterColumn<Guid>(
                name: "interview_guid",
                table: "tb_tr_employee_job",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_employee_job_interview_guid",
                table: "tb_tr_employee_job",
                column: "interview_guid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_employee_job_tb_m_interviews_interview_guid",
                table: "tb_tr_employee_job",
                column: "interview_guid",
                principalTable: "tb_m_interviews",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_employee_job_tb_m_interviews_interview_guid",
                table: "tb_tr_employee_job");

            migrationBuilder.DropIndex(
                name: "IX_tb_tr_employee_job_interview_guid",
                table: "tb_tr_employee_job");

            migrationBuilder.AlterColumn<Guid>(
                name: "interview_guid",
                table: "tb_tr_employee_job",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_employee_job_interview_guid",
                table: "tb_tr_employee_job",
                column: "interview_guid",
                unique: true,
                filter: "[interview_guid] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_employee_job_tb_m_interviews_interview_guid",
                table: "tb_tr_employee_job",
                column: "interview_guid",
                principalTable: "tb_m_interviews",
                principalColumn: "guid");
        }
    }
}
