using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;

namespace Shop.DataAccess;

public class ShopDbContext:DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=ENEL\SQLEXPRESS;Database=Shop;Trusted_Connection=true;");
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<ProductInvoice> ProductInvoice { get; set; }
    public DbSet<CartProduct> CartProduct { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Wallets)
            .WithOne(w => w.User)
            .HasForeignKey(w => w.UserId);
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
            .HasMany(p => p.ProductInvoices)
            .WithOne(pi => pi.Product)
            .HasForeignKey(pi => pi.ProductId);
        modelBuilder.Entity<Invoice>()
            .HasMany(i => i.ProductInvoices)
            .WithOne(pi => pi.Invoice)
            .HasForeignKey(pi => pi.InvoiceId);
        modelBuilder.Entity<Invoice>()
            .HasOne(i => i.Wallet)
            .WithMany(w => w.Invoices)
            .HasForeignKey(i => i.WalletId);
        modelBuilder.Entity<ProductInvoice>()
            .HasKey(pi => new { pi.InvoiceId, pi.ProductId });
        modelBuilder.Entity<CartProduct>()
            .HasKey(cp => new { cp.CartId, cp.ProductId });
    }
}
