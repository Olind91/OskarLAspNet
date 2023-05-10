using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OskarLAspNet.Models.Entities;
using OskarLAspNet.Models.Identity;

namespace OskarLAspNet.Contexts
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<AddressEntity> Adresses { get; set; }
        public DbSet<UserAddressEntity> UserAddresses { get; set; }
    }
}
