using System;
using System.Collections.Generic;
using KP_Test2.Entities;
using Microsoft.EntityFrameworkCore;


namespace KP_Test2.EF;

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

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Historyorder> Historyorders { get; set; }

    public virtual DbSet<Passenger> Passengers { get; set; }

    public virtual DbSet<Usertaxi> Usertaxis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Taxi_KP;Username=postgres;Password=401330");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Idcar).HasName("car_pkey");

            entity.ToTable("car");

            entity.Property(e => e.Idcar).HasColumnName("idcar");
            entity.Property(e => e.Isautopark).HasColumnName("isautopark");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .HasColumnName("model");
            entity.Property(e => e.Numbers)
                .HasMaxLength(10)
                .HasColumnName("numbers");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.Iddriver).HasName("driver_pkey");

            entity.ToTable("driver");

            entity.Property(e => e.Iddriver).HasColumnName("iddriver");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.Idcar).HasColumnName("idcar");
            entity.Property(e => e.Iduser).HasColumnName("iduser");
            entity.Property(e => e.Iswork).HasColumnName("iswork");
            entity.Property(e => e.License).HasColumnName("license");
            entity.Property(e => e.Plane)
                .HasMaxLength(50)
                .HasColumnName("plane");
            entity.Property(e => e.Rating).HasColumnName("rating");

            entity.HasOne(d => d.IdcarNavigation).WithMany(p => p.Drivers)
                .HasForeignKey(d => d.Idcar)
                .HasConstraintName("driver_idcar_fkey");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.Drivers)
                .HasForeignKey(d => d.Iduser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("driver_iduser_fkey");
        });

        modelBuilder.Entity<Historyorder>(entity =>
        {
            entity.HasKey(e => e.Idorder).HasName("historyorder_pkey");

            entity.ToTable("historyorder");

            entity.Property(e => e.Idorder).HasColumnName("idorder");
            entity.Property(e => e.Iddriver).HasColumnName("iddriver");
            entity.Property(e => e.Idpassenger).HasColumnName("idpassenger");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Routeend)
                .HasMaxLength(250)
                .HasColumnName("routeend");
            entity.Property(e => e.Routestart)
                .HasMaxLength(250)
                .HasColumnName("routestart");
            entity.Property(e => e.Timeend)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("timeend");
            entity.Property(e => e.Timestart)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("timestart");

            entity.HasOne(d => d.IddriverNavigation).WithMany(p => p.Historyorders)
                .HasForeignKey(d => d.Iddriver)
                .HasConstraintName("historyorder_iddriver_fkey");

            entity.HasOne(d => d.IdpassengerNavigation).WithMany(p => p.Historyorders)
                .HasForeignKey(d => d.Idpassenger)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("historyorder_idpassenger_fkey");
        });

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.Idpassenger).HasName("passenger_pkey");

            entity.ToTable("passenger");

            entity.Property(e => e.Idpassenger).HasColumnName("idpassenger");
            entity.Property(e => e.Addresshome)
                .HasMaxLength(150)
                .HasColumnName("addresshome");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.Iduser).HasColumnName("iduser");
            entity.Property(e => e.Rating).HasColumnName("rating");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.Passengers)
                .HasForeignKey(d => d.Iduser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("passenger_iduser_fkey");
        });

        modelBuilder.Entity<Usertaxi>(entity =>
        {
            entity.HasKey(e => e.Iduser).HasName("usertaxi_pkey");

            entity.ToTable("usertaxi");

            entity.Property(e => e.Iduser).HasColumnName("iduser");
            entity.Property(e => e.Card).HasColumnName("card");
            entity.Property(e => e.Contact)
                .HasMaxLength(100)
                .HasColumnName("contact");
            entity.Property(e => e.Dateregistration).HasColumnName("dateregistration");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Login)
                .HasMaxLength(100)
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Roletype)
                .HasMaxLength(50)
                .HasColumnName("roletype");
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .HasColumnName("surname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
