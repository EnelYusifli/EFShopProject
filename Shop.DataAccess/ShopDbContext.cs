using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;

namespace Shop.DataAccess;

public class ShopDbContext:DbContext
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
            .HasKey(pd => new {  pd.ProductId, pd.DiscountId });
    }
}
