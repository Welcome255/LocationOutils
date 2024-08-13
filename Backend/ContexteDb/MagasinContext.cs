using System;
using System.Collections.Generic;
using Backend.Entites;
using Microsoft.EntityFrameworkCore;

namespace Backend.ContexteDb;

public partial class MagasinContext : DbContext
{
    public MagasinContext()
    {
    }

    public MagasinContext(DbContextOptions<MagasinContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Equipement> Equipements { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:ConnexionMagasin");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Equipement>(entity =>
        {
            entity.ToTable("Equipement");

            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.Nom).HasMaxLength(50);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("Location");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Equipement).WithMany(p => p.Locations)
                .HasForeignKey(d => d.EquipementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Equipement");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
