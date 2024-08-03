using Employee.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee.Data;

public class ApplicationDbContext : DbContext{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options){
    }

    public DbSet<EmployeeModel> Employees { get; set; } = null!;


    // as these are decimal precisions are added to them. 18 digits and two digit after . are allowed
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeModel>()
            .Property(e => e.BasicSalary)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<EmployeeModel>()
            .Property(e => e.GrossSalary)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<EmployeeModel>()
            .Property(e => e.PercentageOfBasicOnGrossSalary)
            .HasColumnType("decimal(5,2)");
    }
}