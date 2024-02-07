using AutomotiveEcommercePlatform.Server.Data;
using DataBase_LastTesting.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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
            .HasKey(k => new { k.CarId, k.UserId });


            modelBuilder.Entity<Car>() //configuring the relation between the cart and the car 
                .HasMany(c => c.Carts)
                .WithMany(c => c.Cars)
            .UsingEntity(t => t.ToTable("CarsInCart"));

            modelBuilder.Entity<Car>(eb => eb.Property(b => b.Price).HasColumnType("Decimal(15,2)"));

            modelBuilder.Entity<Car>()
                .Property(c => c.OrderId)
                .IsRequired(false);

            modelBuilder.Entity<CarReview>()
                .Property(c => c.Comment)
                .IsRequired(false);

            modelBuilder.Entity<User>()
                .HasOne(pt => pt.Trader)
                .WithOne()
                .HasForeignKey<User>(k => k.TraderId)
                .HasPrincipalKey<Trader>(k => k.TraderId);
               

            modelBuilder.Entity<User>()
                .Property(c => c.TraderId)
                .IsRequired(false);
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Trader> Traders { get; set; }
    }
}