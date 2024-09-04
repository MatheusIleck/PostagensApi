using Microsoft.EntityFrameworkCore;
using PostagensApi.Models;

namespace PostagensApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        public DbSet<Post> Post { get; set; } = null!;

        public DbSet<User> User { get; set; } = null!;

        public DbSet<Like> Likes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseSqlServer(ApiConfiguration.ConnectionString));
        }
    }
}
