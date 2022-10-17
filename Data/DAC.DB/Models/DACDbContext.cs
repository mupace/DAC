using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAC.DB.Models
{
    public partial class DACDBContext : DbContext
    {
        public DACDBContext()
        {
        }

        public DACDBContext(DbContextOptions<DACDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<WorkOrderNote> WorkOrderNotes { get; set; } = null!;
        public virtual DbSet<Workorder> Workorders { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkOrderNote>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasDefaultValueSql("('2022-10-17T00:11:16.2935432Z')");

                entity.HasOne(d => d.WorkOrder)
                    .WithMany(p => p.WorkOrderNotes)
                    .HasForeignKey(d => d.WorkOrderId)
                    .HasConstraintName("FK_WorkOrderNotes_WorkOrders_WorkOrderId");
            });

            modelBuilder.Entity<Workorder>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasDefaultValueSql("('2022-10-17T00:11:16.2920119Z')");

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
