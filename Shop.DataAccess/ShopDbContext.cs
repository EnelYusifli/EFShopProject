using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;
using Shop.Core.Entities.Views;

namespace Shop.DataAccess;

public class ShopDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=ENEL\SQLEXPRESS;Database=Shop;Trusted_Connection=true;");
    }
    public DbSet<User>? Users { get; set; }
    public DbSet<Wallet>? Wallets { get; set; }
    public DbSet<Product>? Products { get; set; }
    public DbSet<Cart>? Carts { get; set; }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Invoice>? Invoices { get; set; }
    public DbSet<Discount>? Discounts { get; set; }
    public DbSet<ProductDiscount>? ProductDiscounts { get; set; }
    public DbSet<ProductInvoice>? ProductInvoices { get; set; }
    public DbSet<CartProduct>? CartProducts { get; set; }
    public DbSet<InvoiceReportProcedure>? InvoiceReport { get; set; }
    public DbSet<TheMostAddedProducts>? TheMostAddedProducts { get; set; }
    public DbSet<CanceledInvoiceReport>? CanceledInvoiceReport { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.UserName)
            .IsUnique();
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        modelBuilder.Entity<Wallet>()
            .HasIndex(w => w.Number)
            .IsUnique();
        modelBuilder.Entity<Product>()
            .HasIndex(p => p.Name)
            .IsUnique();
        modelBuilder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();
        modelBuilder.Entity<User>()
            .HasMany(u => u.Wallets)
            .WithOne(w => w.User);
        modelBuilder.Entity<User>()
            .HasOne(u => u.Cart)
            .WithOne(c => c.User)
            .HasForeignKey<Cart>(c => c.Id);
        modelBuilder.Entity<Cart>()
            .HasMany(c => c.CartProducts)
            .WithOne(cp => cp.Cart)
            .HasForeignKey(cp => cp.CartId);
        modelBuilder.Entity<Product>()
            .HasMany(p => p.CartProducts)
            .WithOne(cp => cp.Product)
            .HasForeignKey(cp => cp.ProductId);
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);
        modelBuilder.Entity<Product>()
            .HasMany(p => p.ProductInvoices)
            .WithOne(pi => pi.Product)
            .HasForeignKey(pi => pi.ProductId);
        modelBuilder.Entity<Invoice>()
            .HasMany(i => i.ProductInvoices)
            .WithOne(pi => pi.Invoice)
            .HasForeignKey(pi => pi.InvoiceId);
        modelBuilder.Entity<Product>()
            .HasMany(p => p.ProductDiscounts)
            .WithOne(pd => pd.Product)
            .HasForeignKey(pd => pd.ProductId);
        modelBuilder.Entity<Discount>()
            .HasMany(d => d.ProductDiscounts)
            .WithOne(pd => pd.Discount)
            .HasForeignKey(pd => pd.DiscountId);
        modelBuilder.Entity<Invoice>()
            .HasOne(i => i.Wallet)
            .WithMany(w => w.Invoices)
            .HasForeignKey(i => i.WalletId);
        modelBuilder.Entity<ProductInvoice>()
            .HasKey(pi => new { pi.InvoiceId, pi.ProductId });
        modelBuilder.Entity<CartProduct>()
            .HasKey(cp => new { cp.CartId, cp.ProductId });
        modelBuilder.Entity<ProductDiscount>()
            .HasKey(pd => new { pd.ProductId, pd.DiscountId });
        modelBuilder.Entity<InvoiceReportProcedure>()
            .ToTable(nameof(InvoiceReportProcedure), t => t.ExcludeFromMigrations()).HasNoKey();
        modelBuilder.Entity<TheMostAddedProducts>()
            .ToTable(nameof(TheMostAddedProducts), t => t.ExcludeFromMigrations()).HasNoKey();
        modelBuilder.Entity<CanceledInvoiceReport>()
            .ToTable(nameof(CanceledInvoiceReport), t => t.ExcludeFromMigrations()).HasNoKey();
    }
    public IEnumerable<InvoiceReportProcedure> GetInvoiceReport(DateTime startTime, DateTime endTime)
    {
        if (startTime < endTime)
            return InvoiceReport.FromSqlInterpolated($"EXEC usp_GetInvoiceReport {startTime}, {endTime}").ToList();
        else throw new Exception("Start Time Cannot be after End Time");
    }
    public IEnumerable<CanceledInvoiceReport> GetCanceledInvoiceReport(DateTime startTime, DateTime endTime)
    {
        if (startTime < endTime)
            return CanceledInvoiceReport.FromSqlInterpolated($"EXEC usp_GetCanceledInvoiceReport {startTime}, {endTime}").ToList();
        else throw new Exception("Start Time Cannot be after End Time");
    }
    public IEnumerable<TheMostAddedProducts> GetTheMostAddedProducts(int count)
    {
        if (count <= 0) throw new Exception("Count cannot be negative or 0");
        return TheMostAddedProducts.FromSqlInterpolated($"EXEC usp_GetTheMostAddedProducts {count}").ToList();
    }
}
