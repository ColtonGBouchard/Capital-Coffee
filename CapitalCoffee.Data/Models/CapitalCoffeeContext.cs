using System.Data.Entity;

namespace CapitalCoffee.Data.Models
{
    public class CapitalCoffeeContext : DbContext
    {
        public CapitalCoffeeContext() : base("name=DefaultConnection")
        {

        }

        public DbSet<Shop> Shops { get; set; }
        
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewPicture> ReviewPictures { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<ProfilePicture> ProfilePictures { get; set; }
        public DbSet<DefaultShopPicture> DefaultShopPictures { get; set; }
        public DbSet<HoursOfOperation> HoursOfOperation { get; set; }

        public System.Data.Entity.DbSet<CapitalCoffee.Data.Models.Role> Roles { get; set; }
    }
}
