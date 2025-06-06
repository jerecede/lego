using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace lego.model;

public partial class LegoContext : DbContext
{
    public LegoContext()
    {
    }

    public LegoContext(DbContextOptions<LegoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LegoColor> LegoColors { get; set; }

    public virtual DbSet<LegoInventory> LegoInventories { get; set; }

    public virtual DbSet<LegoInventoryPart> LegoInventoryParts { get; set; }

    public virtual DbSet<LegoInventorySet> LegoInventorySets { get; set; }

    public virtual DbSet<LegoPart> LegoParts { get; set; }

    public virtual DbSet<LegoPartCategory> LegoPartCategories { get; set; }

    public virtual DbSet<LegoSet> LegoSets { get; set; }

    public virtual DbSet<LegoTheme> LegoThemes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(AppConf.GetConnectionString());
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LegoColor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("lego_colors_pkey");

            entity.ToTable("lego_colors");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsTrans)
                .HasMaxLength(1)
                .HasColumnName("is_trans");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Rgb)
                .HasMaxLength(6)
                .HasColumnName("rgb");
        });

        modelBuilder.Entity<LegoInventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("lego_inventories_pkey");

            entity.ToTable("lego_inventories");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SetNum)
                .HasMaxLength(255)
                .HasColumnName("set_num");
            entity.Property(e => e.Version).HasColumnName("version");

            entity.HasOne(d => d.SetNumNavigation).WithMany(p => p.LegoInventories)
                .HasForeignKey(d => d.SetNum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("inventories_sets_fk");
        });

        modelBuilder.Entity<LegoInventoryPart>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("lego_inventory_parts");

            entity.Property(e => e.ColorId).HasColumnName("color_id");
            entity.Property(e => e.InventoryId).HasColumnName("inventory_id");
            entity.Property(e => e.IsSpare).HasColumnName("is_spare");
            entity.Property(e => e.PartNum)
                .HasMaxLength(255)
                .HasColumnName("part_num");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Color).WithMany()
                .HasForeignKey(d => d.ColorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("inv_parts_color_fk");

            entity.HasOne(d => d.Inventory).WithMany()
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("inv_parts_inventory_fk");

            entity.HasOne(d => d.PartNumNavigation).WithMany()
                .HasForeignKey(d => d.PartNum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("inv_parts_parts_fk");
        });

        modelBuilder.Entity<LegoInventorySet>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("lego_inventory_sets");

            entity.Property(e => e.InventoryId).HasColumnName("inventory_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SetNum)
                .HasMaxLength(255)
                .HasColumnName("set_num");

            entity.HasOne(d => d.Inventory).WithMany()
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("inv_sets_inventory_fk");

            entity.HasOne(d => d.SetNumNavigation).WithMany()
                .HasForeignKey(d => d.SetNum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("inv_sets_sets_fk");
        });

        modelBuilder.Entity<LegoPart>(entity =>
        {
            entity.HasKey(e => e.PartNum).HasName("lego_parts_pkey");

            entity.ToTable("lego_parts");

            entity.Property(e => e.PartNum)
                .HasMaxLength(255)
                .HasColumnName("part_num");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.PartCatId).HasColumnName("part_cat_id");

            entity.HasOne(d => d.PartCat).WithMany(p => p.LegoParts)
                .HasForeignKey(d => d.PartCatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("part_categories_fk");
        });

        modelBuilder.Entity<LegoPartCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("lego_part_categories_pkey");

            entity.ToTable("lego_part_categories");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<LegoSet>(entity =>
        {
            entity.HasKey(e => e.SetNum).HasName("lego_sets_pkey");

            entity.ToTable("lego_sets");

            entity.Property(e => e.SetNum)
                .HasMaxLength(255)
                .HasColumnName("set_num");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.NumParts).HasColumnName("num_parts");
            entity.Property(e => e.ThemeId).HasColumnName("theme_id");
            entity.Property(e => e.Year).HasColumnName("year");

            entity.HasOne(d => d.Theme).WithMany(p => p.LegoSets)
                .HasForeignKey(d => d.ThemeId)
                .HasConstraintName("sets_themes_fk");
        });

        modelBuilder.Entity<LegoTheme>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("lego_themes_pkey");

            entity.ToTable("lego_themes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("parent_themes_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
