using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "a71a55d6-99d7-4123-b4e0-1218ecb90e3e";
            var writerRoleId = "c309fa92-2123-47be-b397-a1c77adb502c";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };


            // database a IdentityRole push korbo

            /*
             
            builder.Entity<IdentityRole>(): This part tells the program that we are working with the IdentityRole entity,
            which represents roles like "Admin," "User," etc., in the system.

            HasData(roles): This method is used to seed data, meaning it's adding predefined roles to the database. 
            The roles variable likely holds a list of roles (for example, "Admin," "Manager," "User") 
            that are inserted into the database when it's created or updated.

             * */

            builder.Entity<IdentityRole>().HasData(roles);

        }
    }
}
