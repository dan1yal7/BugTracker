using Microsoft.EntityFrameworkCore;

namespace Bug_Tracker.Models
{
    public class ApplicationDbContext: DbContext 
    { 
        public ApplicationDbContext(DbContextOptions options): base (options)
        {

        }

         public DbSet <Bug> Bugs { get; set; } 
        public DbSet <Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; } 

    }
}
