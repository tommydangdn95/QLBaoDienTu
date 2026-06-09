using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QLBaoDienTu.Models._Users;
using System.Reflection.Emit;

namespace QLBaoDienTu.Models
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid,
        IdentityUserClaim<Guid>, IdentityUserRole<Guid>,
        IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>,
        IdentityUserToken<Guid>>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        /// <summary>
        /// DbSet cho các bảng News domain (tạo sau)
        /// </summary>
        public DbSet<Category> Categories { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<RelatedNews> RelatedNews { get; set; }
        public DbSet<NewsImage> NewsImages { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Slug).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.HasIndex(e => e.Slug).IsUnique();
                entity.HasIndex(e => e.IsActive);
            });

            // ===== NEWS CONFIGURATION =====
            modelBuilder.Entity<News>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(300);
                entity.Property(e => e.Slug).IsRequired().HasMaxLength(300);
                entity.Property(e => e.Summary).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Content).IsRequired();
                entity.Property(e => e.RejectionReason).HasMaxLength(500);

                // Indexes
                entity.HasIndex(e => e.CategoryId);
                entity.HasIndex(e => e.SubmittedByUserId);
                entity.HasIndex(e => e.ApprovedByAdminId);
                entity.HasIndex(e => e.Slug).IsUnique();
                entity.HasIndex(e => new { e.Status, e.PublishedDate });
                entity.HasIndex(e => e.IsFeatured);

            });

            // ===== NEWSIMAGE CONFIGURATION =====
            modelBuilder.Entity<NewsImage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ImageUrl).IsRequired().HasMaxLength(500);
                entity.Property(e => e.AlternativeText).HasMaxLength(200);
                entity.Property(e => e.Caption).HasMaxLength(500);
                entity.HasIndex(e => e.NewsId);
            });

            // ===== RELATEDNEWS CONFIGURATION =====
            modelBuilder.Entity<RelatedNews>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.MainNewsId);
                entity.HasIndex(e => e.RelatedNewsId);

                // Unique constraint - không trùng lặp
                entity.HasIndex(e => new { e.MainNewsId, e.RelatedNewsId }).IsUnique();
            });

            // ===== COMMENT CONFIGURATION =====
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.AuthorName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.AuthorEmail).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Content).IsRequired();

                entity.HasIndex(e => e.NewsId);
                entity.HasIndex(e => e.IsApproved);
                entity.HasIndex(e => e.CreatedDate);
            });
        }

    }
}
