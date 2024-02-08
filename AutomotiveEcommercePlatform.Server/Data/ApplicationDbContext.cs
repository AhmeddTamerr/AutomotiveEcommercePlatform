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
                .HasForeignKey<User>(c => c.ApplicationUserId)
                .HasPrincipalKey<ApplicationUser>(c => c.Id);
            

            modelBuilder.Entity<Order>()
                .Property(p => p.PurchaseDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<User>()
                .Property(p => p.DisplayName)
                .HasComputedColumnSql("[FirstName] + ' ' + [LastName]");

            modelBuilder.Entity<Trader>()
                .Property(p => p.DisplayName)
                .HasComputedColumnSql("[FirstName] + ' ' + [LastName]");

            modelBuilder.Entity<User>()
                .HasKey(c => c.ApplicationUserId);

            modelBuilder.Entity<Cart>()
            .HasKey(k => new { k.CarId, k.CartId});

            modelBuilder.Entity<CarsInTheCart>()
                .HasKey(t => new { t.CarId, t.CartId});

            modelBuilder.Entity<CarsInTheCart>()
                .HasOne(pt => pt.Car)
                .WithMany(p => p.CarsInTheCart)
                .HasForeignKey(f => f.CarId)
                .HasPrincipalKey(p => p.Id);

            modelBuilder.Entity<CarsInTheCart>()
                .HasOne(o => o.Cart)
                .WithMany(m => m.CarsInTheCart)
                .HasForeignKey(k => k.CartId)
                .HasPrincipalKey(p => p.CartId);

            modelBuilder.Entity<Cart>()
                .HasOne(p => p.User)
                .WithOne(c => c.Cart)
                .HasForeignKey<Cart>(k => k.CartId)
                .HasPrincipalKey<User>(p => p.ApplicationUserId);


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

            //modelBuilder.Entity<User>()
            //    .Property(pt => pt.ApplicationUserId)
            //    .HasColumnName("UserId");


        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Trader> Traders { get; set; }
    }
}