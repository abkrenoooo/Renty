using DAL.Entities;
 using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Renty.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Address> Address { get; set; }
        public DbSet<Locations> Locations { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("Users", "security");
            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");


            //builder.Entity<Notifications>()
            //.HasOne(f => f.Reciver)
            //.WithMany(f => f.ReciverNotifications);

            //builder.Entity<Order>()
            //   .HasOne(f => f.Customer)
            //   .WithMany(f => f.CustomerOrders);
            
            //builder.Entity<Order>()
            //   .HasOne(f => f.Customer)
            //   .WithMany(f => f.CustomerOrders);

           
        }
    }
}
