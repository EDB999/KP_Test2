using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KP_Test2;

public partial class TaxiKpContext : DbContext
{
    public TaxiKpContext()
    {
    }

    public TaxiKpContext(DbContextOptions<TaxiKpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Taxi_KP;Username=postgres;Password=401330");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.IdCars).HasName("Cars_pkey");

            entity.Property(e => e.IdCars).HasColumnName("ID_Cars");
            entity.Property(e => e.Model).HasColumnType("character varying");
            entity.Property(e => e.Number).HasColumnType("character varying");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("Clients_pkey");

            entity.Property(e => e.IdClient).HasColumnName("ID_Client");
            entity.Property(e => e.Contact).HasColumnType("character varying");
            entity.Property(e => e.Card).HasColumnType("integer");
            entity.Property(e => e.Email).HasColumnType("character varying");
            entity.Property(e => e.Login).HasColumnType("character varying");
            entity.Property(e => e.Name).HasColumnType("character varying");
            entity.Property(e => e.PassEntry)
                .HasColumnType("character varying")
                .HasColumnName("Pass_Entry");
            entity.Property(e => e.Password).HasColumnType("character varying");
            entity.Property(e => e.Surname).HasColumnType("character varying");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.IdDriver).HasName("Drivers_pkey");

            entity.Property(e => e.IdDriver).HasColumnName("ID_Driver");
            entity.Property(e => e.Contact).HasColumnType("character varying");
            entity.Property(e => e.Login).HasColumnType("character varying");
            entity.Property(e => e.Name).HasColumnType("character varying");
            entity.Property(e => e.PassEntry)
                .HasColumnType("character varying")
                .HasColumnName("Pass_Entry");
            entity.Property(e => e.PassportNumber).HasColumnName("Passport_Number");
            entity.Property(e => e.PassportSeria).HasColumnName("Passport_Seria");
            entity.Property(e => e.Password).HasColumnType("character varying");
            entity.Property(e => e.Surname).HasColumnType("character varying");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrders).HasName("Orders_pkey");

            entity.Property(e => e.IdOrders).HasColumnName("ID_Orders");
            entity.Property(e => e.Address).HasColumnType("character varying");
            entity.Property(e => e.Time).HasColumnType("character varying");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
