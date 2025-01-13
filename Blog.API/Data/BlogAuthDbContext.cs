using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Data
{
    public class BlogAuthDbContext : IdentityDbContext
    {
        public BlogAuthDbContext(DbContextOptions<BlogAuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "2fb2191c-1914-4588-a05c-d3ce64c4a0a1";
            var writerRoleId = "9d63458b-36de-4537-96e9-1a82beac26eb";

            // create reader and writer role
            var roles = new List<IdentityRole>() {

                new IdentityRole
                {
                    Id= readerRoleId,
                    Name ="Reader",
                     NormalizedName = "Reader".ToUpper(),
                     ConcurrencyStamp = readerRoleId
                },
                    new IdentityRole
                {
                    Id= writerRoleId,
                    Name ="Writer",
                     NormalizedName = "Writer".ToUpper(),
                     ConcurrencyStamp = readerRoleId
                }

            };

            //seed the role

            builder.Entity<IdentityRole>().HasData(roles);

            //create admin

            var adminUserId = "fc1fda87-f746-4066-a25e-23774394fd80";

            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "abc@blog.com",
                Email = "abc@blog.com",
                NormalizedEmail = "abc@blog.com".ToUpper(),
                NormalizedUserName = "abc@blog.com".ToUpper()
            };
            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");

            builder.Entity<IdentityUser>().HasData(admin);

            //give roles to admin

            var adminRoles = new List<IdentityUserRole<string>>
            {
                new()
                {
                    RoleId = readerRoleId,
                    UserId = admin.Id,
                },
                new()
                {
                    RoleId= writerRoleId,
                    UserId = admin.Id,
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);

        }
    }
}
