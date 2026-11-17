using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RamzecAPI.Models.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Deal> Deals { get; set; }

    public virtual DbSet<Furniture> Furnitures { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerCode).HasName("customers_pk");

            entity.ToTable("customers");

            entity.Property(e => e.CustomerCode).HasColumnName("customer_code");
            entity.Property(e => e.CustomerAdress)
                .HasColumnType("character varying")
                .HasColumnName("customer_adress");
            entity.Property(e => e.CustomerName)
                .HasColumnType("character varying")
                .HasColumnName("customer_name");
            entity.Property(e => e.CustomerPhone).HasColumnName("customer_phone");
        });

        modelBuilder.Entity<Deal>(entity =>
        {
            entity.HasKey(e => e.DealNumber).HasName("deals_number_pk");

            entity.ToTable("deals");

            entity.Property(e => e.DealNumber)
                .ValueGeneratedNever()
                .HasColumnName("deal_number");
            entity.Property(e => e.CustomerCode)
                .ValueGeneratedOnAdd()
                .HasColumnName("customer_code");
            entity.Property(e => e.DealDateDone)
                .HasColumnType("character varying")
                .HasColumnName("deal_date_done");
            entity.Property(e => e.DealDateMade)
                .HasColumnType("character varying")
                .HasColumnName("deal_date_made");

            entity.HasOne(d => d.CustomerCodeNavigation).WithMany(p => p.Deals)
                .HasForeignKey(d => d.CustomerCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("deals_customers_fk");
        });

        modelBuilder.Entity<Furniture>(entity =>
        {
            entity.HasKey(e => e.FurnitureName).HasName("furniture_name_pk");

            entity.ToTable("furniture");

            entity.HasIndex(e => e.FurnitureModel, "furniture_unique").IsUnique();

            entity.Property(e => e.FurnitureName)
                .HasColumnType("character varying")
                .HasColumnName("furniture_name");
            entity.Property(e => e.FurnitureCharacteristics)
                .HasColumnType("character varying")
                .HasColumnName("furniture_characteristics");
            entity.Property(e => e.FurnitureCost).HasColumnName("furniture_cost");
            entity.Property(e => e.FurnitureModel)
                .HasColumnType("character varying")
                .HasColumnName("furniture_model");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.Saleid).HasName("sales_pk");

            entity.ToTable("sales");

            entity.Property(e => e.Saleid).HasColumnName("saleid");
            entity.Property(e => e.DealNumber).HasColumnName("deal_number");
            entity.Property(e => e.FurnitureModel)
                .HasColumnType("character varying")
                .HasColumnName("furniture_model");
            entity.Property(e => e.FurnitureName)
                .HasColumnType("character varying")
                .HasColumnName("furniture_name");
            entity.Property(e => e.SalesAmount).HasColumnName("sales_amount");

            entity.HasOne(d => d.DealNumberNavigation).WithMany(p => p.Sales)
                .HasForeignKey(d => d.DealNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sales_deals_fk");

            entity.HasOne(d => d.FurnitureModelNavigation).WithMany(p => p.SaleFurnitureModelNavigations)
                .HasPrincipalKey(p => p.FurnitureModel)
                .HasForeignKey(d => d.FurnitureModel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sales_furniture_model_FK");

            entity.HasOne(d => d.FurnitureNameNavigation).WithMany(p => p.SaleFurnitureNameNavigations)
                .HasForeignKey(d => d.FurnitureName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sales_furniture_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
