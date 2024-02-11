using AutomotiveEcommercePlatform.Server.Data;
using DataBase_LastTesting.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using AutomotiveEcommercePlatform.Server.Models;

namespace ReactApp1.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            modelBuilder.Entity<User>()
                .HasOne<ApplicationUser>()
                .WithOne()
                .HasForeignKey<User>(c => c.UserId)
                .HasPrincipalKey<ApplicationUser>(c => c.Id);
            
            modelBuilder.Entity<Trader>()
                .HasOne<ApplicationUser>()
                .WithOne()
                .HasForeignKey<Trader>(c => c.TraderId)
                .HasPrincipalKey<ApplicationUser>(c => c.Id);
            

            modelBuilder.Entity<Order>()
                .Property(p => p.PurchaseDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<ApplicationUser>()
                .Property(p => p.DisplayName)
                .HasComputedColumnSql("[FirstName] + ' ' + [LastName]");

            modelBuilder.Entity<User>()
                .HasKey(c => c.UserId);

            modelBuilder.Entity<Trader>()
                .HasKey(c => c.TraderId);

            modelBuilder.Entity<Cart>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Cart>(c => c.CartId)
                .HasPrincipalKey<User>(c => c.UserId);

            modelBuilder.Entity<Cart>()
                .HasKey(k => k.CartId);


            /*modelBuilder.Entity<Car>() //configuring the relation between the cart and the car 
                .HasMany(c => c.Cart)
                .WithMany(c => c.Car)
            .UsingEntity(t => t.ToTable("CarsInCart"));*/

            // Set a true default value for InStock Property 
            modelBuilder.Entity<Car>()
                .Property(e => e.InStock)
                .HasDefaultValue(true);

            modelBuilder.Entity<Car>(eb => eb.Property(b => b.Price).HasColumnType("Decimal(15,2)"));

            modelBuilder.Entity<Car>()
                .Property(c => c.OrderId)
                .IsRequired(false);

            modelBuilder.Entity<CarReview>()
                .Property(c => c.Comment)
                .IsRequired(false);

            modelBuilder.Entity<CarReview>()
                .Property(c => c.UserId)
                .IsRequired(true);


            modelBuilder.Entity<Order>()
                .Property(c => c.UserId)
                .IsRequired(true);

            /*modelBuilder.Entity<User>()
                .HasOne(pt => pt.Trader)
                .WithOne()
                .HasForeignKey<User>(k => k.TraderId)
                .HasPrincipalKey<Trader>(k => k.TraderId);*/


            /*modelBuilder.Entity<User>()
                .Property(c => c.TraderId)
                .IsRequired(false);*/

            // Cart ,  Car with cartItems 

            modelBuilder.Entity<Cart>()
                .HasMany(c => c.Car)
                .WithMany(t => t.Cart)
                .UsingEntity<CartItem>(
                    j =>
                    {
                        j
                            .HasOne(c => c.Cart)
                            .WithMany(t => t.CartItems)
                            .HasForeignKey(pt => pt.CartId);

                        j
                            .HasOne(c => c.Car)
                            .WithMany(t => t.CartItems)
                            .HasForeignKey(pt => pt.CarId);

                        j.HasKey(t => new { t.CartId, t.CarId });
                    }
                );
            modelBuilder.Entity<Trader>()
                .HasMany(c => c.User)
                .WithMany(t => t.Trader)
                .UsingEntity<TraderRating>(
                    j =>
                    {
                        j
                            .HasOne(c => c.Trader)
                            .WithMany(t => t.TraderRatings)
                            .HasForeignKey(pt => pt.TraderId);

                        j
                            .HasOne(c => c.User)
                            .WithMany(t => t.TradersRatings)
                            .HasForeignKey(pt => pt.UserId);

                        j.HasKey(t => t.Id);
                    }
                );
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarReview> CarReviews { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Trader> Traders { get; set; }
        public DbSet<TraderRating> TraderRatings { get; set; }
        public DbSet<User> Users { get; set; }
        
        
    }
}