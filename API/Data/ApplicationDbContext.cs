using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    //Tables
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeInterview> EmployeeInterviews { get; set; }
    public DbSet<EmployeeProject> EmployeeProjects { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Interview> Interviews { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Placement> Placements { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Role> Roles { get; set; }

    // Other Configuration or Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Constraints Unique
        modelBuilder.Entity<Employee>().HasIndex(e => new
        {
            e.Nik,
            e.Email,
            e.PhoneNumber
        }).IsUnique();

        // Account - AccountRole (One to Many)
        modelBuilder.Entity<Account>()
            .HasMany(account => account.AccountRoles)
            .WithOne(accountRole => accountRole.Account)
            .HasForeignKey(accountRole => accountRole.AccountGuid);

        // Account - Employee (One to One)
        modelBuilder.Entity<Account>()
            .HasOne(account => account.Employee)
            .WithOne(employee => employee.Account)
            .HasForeignKey<Account>(account => account.Guid);

        // Company - Job (One to Many)
        modelBuilder.Entity<Company>()
            .HasMany(company => company.Jobs)
            .WithOne(job => job.Company)
            .HasForeignKey(job => job.CompanyGuid);

        // Company - Placement (One to Many)
        modelBuilder.Entity<Company>()
            .HasMany(company => company.Placements)
            .WithOne(placement => placement.Company)
            .HasForeignKey(placement => placement.CompanyGuid);

        // Employee - EmployeeInterview (One to Many)
        modelBuilder.Entity<Employee>()
            .HasMany(employee => employee.EmployeeInterviews)
            .WithOne(employeeInterview => employeeInterview.Employee)
            .HasForeignKey(employeeInterview => employeeInterview.EmployeeGuid);

        // Employee - EmployeeProject (One to Many)
        modelBuilder.Entity<Employee>()
            .HasMany(employee => employee.EmployeeProjects)
            .WithOne(employeeProject => employeeProject.Employee)
            .HasForeignKey(employeeProject => employeeProject.EmployeeGuid);

        // Employee - Grade (One to One)
        modelBuilder.Entity<Employee>()
            .HasOne(employee => employee.Grade)
            .WithOne(grade => grade.Employee)
            .HasForeignKey<Employee>(employee => employee.GradeGuid);

        // Employee - Placement (One to One)
        modelBuilder.Entity<Employee>()
            .HasOne(employee => employee.Placement)
            .WithOne(placement => placement.Employee)
            .HasForeignKey<Placement>(placement => placement.EmployeeGuid);

        // Employee - Profile (One to One)
        modelBuilder.Entity<Employee>()
            .HasOne(employee => employee.Profile)
            .WithOne(profile => profile.Employee)
            .HasForeignKey<Employee>(employee => employee.ProfileGuid);

        // Interview - Job (One to Many)
        modelBuilder.Entity<Interview>()
            .HasOne(interview => interview.Job)
            .WithMany(job => job.Interviews)
            .HasForeignKey(interview => interview.JobGuid);

        // Interview - EmployeeInterview (One to Many)
        modelBuilder.Entity<Interview>()
            .HasMany(interview => interview.EmployeeInterviews)
            .WithOne(employeeInterview => employeeInterview.Interview)
            .HasForeignKey(employeeInterview => employeeInterview.InterviewGuid);

        // Project - EmployeeProject (One to Many)
        modelBuilder.Entity<Project>()
            .HasMany(project => project.EmployeeProjects)
            .WithOne(employeeProject => employeeProject.Project)
            .HasForeignKey(employeeProject => employeeProject.ProjectGuid);

        // Role - AccountRole (One to Many)
        modelBuilder.Entity<Role>()
            .HasMany(role => role.AccountRoles)
            .WithOne(accoutRole => accoutRole.Role)
            .HasForeignKey(accountRole => accountRole.RoleGuid);
    }
}
