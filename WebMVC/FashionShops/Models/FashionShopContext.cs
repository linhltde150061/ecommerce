using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FashionShops.Models;

public partial class FashionShopContext : DbContext
{
    public FashionShopContext()
    {
    }

    public FashionShopContext(DbContextOptions<FashionShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<NewsCategory> NewsCategories { get; set; }

    public virtual DbSet<NewsComment> NewsComments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductBrand> ProductBrands { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductColor> ProductColors { get; set; }

    public virtual DbSet<ProductDetail> ProductDetails { get; set; }

    public virtual DbSet<ProductSize> ProductSizes { get; set; }

    public virtual DbSet<ProductVoting> ProductVotings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=Linh;Initial Catalog=FashionShop;Persist Security Info=True;User ID=sa;Password=12345;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>(entity =>
        {
            entity.ToTable("Member");

            entity.Property(e => e.Avatar).HasMaxLength(250);
            entity.Property(e => e.Name).HasMaxLength(250);
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.NewsCategoryId).HasColumnName("News_CategoryId");
            entity.Property(e => e.Picture).HasMaxLength(150);
            entity.Property(e => e.Title).HasMaxLength(250);

            entity.HasOne(d => d.CreateByNavigation).WithMany(p => p.News)
                .HasForeignKey(d => d.CreateBy)
                .HasConstraintName("FK_News_Member");

            entity.HasOne(d => d.NewsCategory).WithMany(p => p.News)
                .HasForeignKey(d => d.NewsCategoryId)
                .HasConstraintName("FK_News_News_Category");
        });

        modelBuilder.Entity<NewsCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Category");

            entity.ToTable("News_Category");

            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<NewsComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Comment");

            entity.ToTable("News_Comment");

            entity.Property(e => e.Content).HasMaxLength(500);

            entity.HasOne(d => d.CommentByNavigation).WithMany(p => p.NewsComments)
                .HasForeignKey(d => d.CommentBy)
                .HasConstraintName("FK_Comment_Member");

            entity.HasOne(d => d.News).WithMany(p => p.NewsComments)
                .HasForeignKey(d => d.NewsId)
                .HasConstraintName("FK_Comment_News");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Information).HasMaxLength(500);
            entity.Property(e => e.Picture).HasMaxLength(250);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ProductName).HasMaxLength(250);

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK_Product_Product_Brand");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Product_Product_Category");

            entity.HasOne(d => d.CreateByNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.CreateBy)
                .HasConstraintName("FK_Product_Member");
        });

        modelBuilder.Entity<ProductBrand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Brand");

            entity.ToTable("Product_Brand");

            entity.Property(e => e.BrandName).HasMaxLength(150);
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.ToTable("Product_Category");

            entity.Property(e => e.CategoryName).HasMaxLength(150);
        });

        modelBuilder.Entity<ProductColor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Color");

            entity.ToTable("Product_Color");

            entity.Property(e => e.Color).HasMaxLength(150);
        });

        modelBuilder.Entity<ProductDetail>(entity =>
        {
            entity.ToTable("Product_Detail");

            entity.Property(e => e.Picture).HasMaxLength(250);

            entity.HasOne(d => d.Color).WithMany(p => p.ProductDetails)
                .HasForeignKey(d => d.ColorId)
                .HasConstraintName("FK_Product_Detail_Product_Color");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Product_Detail_Product");

            entity.HasOne(d => d.Size).WithMany(p => p.ProductDetails)
                .HasForeignKey(d => d.SizeId)
                .HasConstraintName("FK_Product_Detail_Product_Size");
        });

        modelBuilder.Entity<ProductSize>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Size");

            entity.ToTable("Product_Size");

            entity.Property(e => e.Size)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<ProductVoting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Voting");

            entity.ToTable("Product_Voting");

            entity.Property(e => e.Review).HasMaxLength(500);

            entity.HasOne(d => d.CreateByNavigation).WithMany(p => p.ProductVotings)
                .HasForeignKey(d => d.CreateBy)
                .HasConstraintName("FK_Product_Voting_Member");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductVotings)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Product_Voting_Product");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
