using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        
        public DbSet<HoursOfOperation> HoursOfOperations { get; set; }
    }
}
