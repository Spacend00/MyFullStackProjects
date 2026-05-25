
using Microsoft.EntityFrameworkCore;
using PostAppAPI.Domain.Entities;

namespace PostAppAPI.Infrastructure.Persistance
{
    public class PostAppAPIDbContext : DbContext
    {
        public PostAppAPIDbContext(DbContextOptions<PostAppAPIDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(p => p.Posts)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Name).HasMaxLength(50);
                entity.Property(u => u.Surname).HasMaxLength(50);
                entity.Property(u => u.Mail).HasMaxLength(100);
                entity.Property(u => u.PasswordHash).IsRequired();
                entity.Property(u => u.PasswordSalt).IsRequired();

                entity.HasIndex(u => u.Mail).IsUnique();
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(p => p.Content).HasMaxLength(280).IsRequired();
            });
        }
    }
}
