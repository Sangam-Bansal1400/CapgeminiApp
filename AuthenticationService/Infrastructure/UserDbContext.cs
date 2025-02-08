using CapgAppLibrary;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Infrastructure
{
    public class UserDbContext:DbContext
    {
        public DbSet<User>Users { get; set; }
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u=>u.UserId);
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    UserName = "admin",
                    Password = "admin",
                    RoleName = "Admin",
                    Email = "admin@example.com",
                },
                    new User
                    {
                        UserId = 2,
                        UserName = "user",
                        Password = "user",
                        RoleName = "User",
                        Email = "user@example.com",
                    }

                );
            base.OnModelCreating(modelBuilder);
        }
       
    }
}
