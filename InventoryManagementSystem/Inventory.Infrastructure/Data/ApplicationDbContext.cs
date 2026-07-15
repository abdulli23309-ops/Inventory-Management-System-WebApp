using Microsoft.EntityFrameworkCore;
using Inventory.Application.Entities;

namespace Inventory.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Entity Relationships and Delete Behaviors
            modelBuilder.Entity<PurchaseDetail>()
              .HasOne(pd => pd.Purchase)
              .WithMany(p => p.Details)
              .HasForeignKey(pd => pd.PurchaseId)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PurchaseDetail>()
              .HasOne(pd => pd.Product)
              .WithMany()
              .HasForeignKey(pd => pd.ProductId)
              .OnDelete(DeleteBehavior.NoAction);

            // 2. Prevent global query filters from breaking queries on soft-deleted entities
            // Making these navigations optional allows LEFT JOINs to preserve historical transaction records
            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.Supplier)
                .WithMany()
                .HasForeignKey(p => p.SupplierId)
                .IsRequired(false);

            modelBuilder.Entity<PurchaseDetail>()
                .HasOne(pd => pd.Product)
                .WithMany()
                .HasForeignKey(pd => pd.ProductId)
                .IsRequired(false);

            modelBuilder.Entity<SaleDetail>()
                .HasOne(sd => sd.Product)
                .WithMany()
                .HasForeignKey(sd => sd.ProductId)
                .IsRequired(false);

            // 3. Decimal Precision Configurations (Including Invoice)
            modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);
            modelBuilder.Entity<Purchase>().Property(p => p.TotalAmount).HasPrecision(18, 2);
            modelBuilder.Entity<PurchaseDetail>().Property(p => p.UnitPrice).HasPrecision(18, 2);
            modelBuilder.Entity<Sale>().Property(s => s.TotalAmount).HasPrecision(18, 2);
            modelBuilder.Entity<SaleDetail>().Property(sd => sd.UnitPrice).HasPrecision(18, 2);
            modelBuilder.Entity<Invoice>().Property(i => i.TaxAmount).HasPrecision(18, 2);
            modelBuilder.Entity<Invoice>().Property(i => i.TotalAmount).HasPrecision(18, 2);

            // 4. Global Query Filters for Soft Delete
            modelBuilder.Entity<Category>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Supplier>().HasQueryFilter(s => !s.IsDeleted);
        }
    }
}