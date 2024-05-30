using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TechMegStore.Models;

namespace TechMegStore.DATAS.DBContext;


//REPRESENTACION DE TODA LAS BASE DE DATO CON TODAS NUESTRAS TABLAS//
public partial class TechMegastoreDbContext : DbContext
{
    public TechMegastoreDbContext()
    {
    }

    public TechMegastoreDbContext(DbContextOptions<TechMegastoreDbContext> options)
        : base(options)
    {
    }

    //TODAS LAS DEFINICIONES DE NUESTRAS TABLAS QUE SE CONOCEN COMO CLASES//
    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<DocumentNumber> DocumentNumbers { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuRole> MenuRoles { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SaleDetail> SaleDetails { get; set; }

    public virtual DbSet<Useer> Useers { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=(local); DataBase=techMegastoreDB; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }



    //PARAMETRIZACION DE LAS COLUMNAS//
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PK__Category__79D361B67B33059D");

            entity.ToTable("Category");

            entity.Property(e => e.IdCategory).HasColumnName("idCategory");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.DateRegistration)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateRegistration");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<DocumentNumber>(entity =>
        {
            entity.HasKey(e => e.IdDocumentNumber).HasName("PK__Document__BB80F590064262BD");

            entity.ToTable("DocumentNumber");

            entity.Property(e => e.IdDocumentNumber).HasColumnName("idDocumentNumber");
            entity.Property(e => e.DateRegistration)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateRegistration");
            entity.Property(e => e.LastNumber).HasColumnName("lastNumber");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu).HasName("PK__Menu__C26AF48346C9A20B");

            entity.ToTable("Menu");

            entity.Property(e => e.IdMenu).HasColumnName("idMenu");
            entity.Property(e => e.Icon)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("icon");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Url)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("url");
        });

        modelBuilder.Entity<MenuRole>(entity =>
        {
            entity.HasKey(e => e.IdMenuRole).HasName("PK__MenuRole__DD79D4CACA8222D8");

            entity.ToTable("MenuRole");

            entity.Property(e => e.IdMenuRole).HasColumnName("idMenuRole");
            entity.Property(e => e.IdMenu).HasColumnName("idMenu");
            entity.Property(e => e.IdRole).HasColumnName("idRole");

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.MenuRoles)
                .HasForeignKey(d => d.IdMenu)
                .HasConstraintName("FK__MenuRole__idMenu__3F466844");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.MenuRoles)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("FK__MenuRole__idRole__403A8C7D");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("PK__Product__5EEC79D10FA9C264");

            entity.ToTable("Product");

            entity.Property(e => e.IdProduct).HasColumnName("idProduct");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.DateRegistration)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateRegistration");
            entity.Property(e => e.IdCategory).HasColumnName("idCategory");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Stock).HasColumnName("stock");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdCategory)
                .HasConstraintName("FK__Product__idCateg__5629CD9C");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PK__Role__E5045C549C768CC5");

            entity.ToTable("Role");

            entity.Property(e => e.IdRole).HasColumnName("idRole");
            entity.Property(e => e.DateRegistration)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateRegistration");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.IdSale).HasName("PK__Sale__C4AEB19849CD9A68");

            entity.ToTable("Sale");

            entity.Property(e => e.IdSale).HasColumnName("idSale");
            entity.Property(e => e.DateRegistration)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateRegistration");
            entity.Property(e => e.DocumentNumber)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("documentNumber");
            entity.Property(e => e.PaymentType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("paymentType");
            entity.Property(e => e.SaleTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("saleTotal");
        });

        modelBuilder.Entity<SaleDetail>(entity =>
        {
            entity.HasKey(e => e.IdSaleDetail).HasName("PK__SaleDeta__B23385CDAFE97D35");

            entity.ToTable("SaleDetail");

            entity.Property(e => e.IdSaleDetail).HasColumnName("idSaleDetail");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.IdProduct).HasColumnName("idProduct");
            entity.Property(e => e.IdSale).HasColumnName("idSale");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.SaleDetails)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK__SaleDetai__idPro__619B8048");

            entity.HasOne(d => d.IdSaleNavigation).WithMany(p => p.SaleDetails)
                .HasForeignKey(d => d.IdSale)
                .HasConstraintName("FK__SaleDetai__idSal__60A75C0F");
        });

        modelBuilder.Entity<Useer>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__Useer__3717C982B7081789");

            entity.ToTable("Useer");

            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.CompleteName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("completeName");
            entity.Property(e => e.DateRegistration)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateRegistration");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IdRole).HasColumnName("idRole");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Useers)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("FK__Useer__idRole__4316F928");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
