// Data/DiagnosticBillingDbContext.cs
using DiagnosticBilling.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class DiagnosticBillingDbContext : DbContext
{
    public DiagnosticBillingDbContext(DbContextOptions<DiagnosticBillingDbContext> options) : base(options) { }

    public DbSet<Patient> Patient { get; set; }
    public DbSet<Doctor> Doctor { get; set; }
    public DbSet<Test> Test { get; set; }
    public DbSet<Bill> Bill { get; set; }
    public DbSet<BillTest> BillTest { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure decimal precision for TotalCost and Price
        modelBuilder.Entity<Bill>()
            .Property(b => b.TotalCost)
            .HasColumnType("decimal(18,2)");  // Adjust precision and scale as necessary

        modelBuilder.Entity<Test>()
            .Property(t => t.Price)
            .HasColumnType("decimal(18,2)");  // Adjust precision and scale as necessary

        // Many-to-many relationship between Bill and Test
        modelBuilder.Entity<BillTest>()
            .HasKey(bt => new { bt.BillId, bt.TestId });

        modelBuilder.Entity<BillTest>()
            .HasOne(bt => bt.Bill)
            .WithMany(b => b.BillTests)
            .HasForeignKey(bt => bt.BillId);

        modelBuilder.Entity<BillTest>()
            .HasOne(bt => bt.Test)
            .WithMany(t => t.BillTests)
            .HasForeignKey(bt => bt.TestId);

        modelBuilder.Entity<Bill>()
            .HasOne(b => b.Patient)
            .WithMany()
            .HasForeignKey(b => b.patientId);

        modelBuilder.Entity<Bill>()
            .HasOne(b => b.Doctor)
            .WithMany()
            .HasForeignKey(b => b.doctorId);
    }


}


