using Blogedium_api.Modals;
using Microsoft.EntityFrameworkCore;

namespace Blogedium_api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<UserModal> Users { get; set; }
        public DbSet<CommentModal> Comments {get; set;}
        public DbSet<BlogModal> Blogs {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModal>()
            .Property(u => u.Role)
            .HasConversion<string>();

            modelBuilder.Entity<CommentModal>()
            .HasOne(c => c.Blog)
            .WithMany(b => b.Comments)
            .HasForeignKey(a => a.BlogId);
        }
    }
}