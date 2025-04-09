using Microsoft.EntityFrameworkCore;
using MyWebApi.Models;

public class ApplicationDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<ProductReview> ProductReviews { get; set; }
    public DbSet<PriceHistory> PriceHistories { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configuration Customer
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Email).IsUnique();
            
            // Relation avec Orders
            entity.HasMany(c => c.Orders)
                  .WithOne(o => o.Customer)
                  .HasForeignKey(o => o.CustomerId)
                  .OnDelete(DeleteBehavior.Restrict);
                  
            // Relation avec Reviews
            entity.HasMany(c => c.Reviews)
                  .WithOne(r => r.Customer)
                  .HasForeignKey(r => r.CustomerId)
                  .OnDelete(DeleteBehavior.Restrict);
                  
            // Configurer les owned entities des adresses
            entity.OwnsOne(c => c.ShippingAddress);
            entity.OwnsOne(c => c.BillingAddress);
        });
        
        // Configuration Category
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            
            // Relation hiérarchique des catégories
            entity.HasOne(c => c.ParentCategory)
                  .WithMany(c => c.SubCategories)
                  .HasForeignKey(c => c.ParentCategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
                  
            // Relation avec Products
            entity.HasMany(c => c.Products)
                  .WithOne(p => p.Category)
                  .HasForeignKey(p => p.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
        
        // Configuration Product
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Price).HasPrecision(18, 2);
            
            // Contrainte de vérification pour le prix
            entity.ToTable(t => t.HasCheckConstraint("CK_Product_Price", "`Price` > 0"));
            
            // Index sur le nom pour la recherche rapide
            entity.HasIndex(e => e.Name);
            
            // Relations
            entity.HasMany(p => p.PriceHistory)
                  .WithOne(h => h.Product)
                  .HasForeignKey(h => h.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);
                  
            entity.HasMany(p => p.Reviews)
                  .WithOne(r => r.Product)
                  .HasForeignKey(r => r.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        
        // Configuration Order
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.OrderNumber).IsRequired().HasMaxLength(20);
            entity.Property(e => e.TotalAmount).HasPrecision(18, 2);
            
            // Index sur le numéro de commande
            entity.HasIndex(e => e.OrderNumber).IsUnique();
            
            // Index composite sur CustomerId et OrderDate pour les recherches
            entity.HasIndex(e => new { e.CustomerId, e.OrderDate });
            
            // Configuration de l'adresse de livraison comme owned entity
            entity.OwnsOne(o => o.ShippingAddress);
            
            // Relation avec OrderItems
            entity.HasMany(o => o.OrderItems)
                  .WithOne(i => i.Order)
                  .HasForeignKey(i => i.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        
        // Configuration OrderItem
        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UnitPrice).HasPrecision(18, 2);
            entity.Property(e => e.TotalPrice).HasPrecision(18, 2);
            entity.Property(e => e.DiscountAmount).HasPrecision(18, 2);
            
            // Contrainte de vérification pour la quantité
            entity.ToTable(t => t.HasCheckConstraint("CK_OrderItem_Quantity", "`Quantity` > 0"));
        });
        
        // Configuration ProductReview
        modelBuilder.Entity<ProductReview>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Rating).IsRequired();
            
            // Contrainte de vérification pour le rating
            entity.ToTable(t => t.HasCheckConstraint("CK_ProductReview_Rating", "`Rating` BETWEEN 1 AND 5"));
            
            // Index composite pour éviter les doublons de reviews
            entity.HasIndex(e => new { e.ProductId, e.CustomerId }).IsUnique();
        });
        
        // Configuration PriceHistory
        modelBuilder.Entity<PriceHistory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.OldPrice).HasPrecision(18, 2);
            entity.Property(e => e.NewPrice).HasPrecision(18, 2);
            
            // Index sur la date de changement pour les recherches chronologiques
            entity.HasIndex(e => e.ChangeDate);
        });
    }
}