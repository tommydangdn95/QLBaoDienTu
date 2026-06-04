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

                // Relationships
                entity.HasOne(e => e.Category)
                    .WithMany(c => c.News)
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.NewsImages)
                    .WithOne(ni => ni.News)
                    .HasForeignKey(ni => ni.NewsId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Comments)
                    .WithOne(c => c.News)
                    .HasForeignKey(c => c.NewsId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Relationship: SubmittedByUser
                entity.HasOne(e => e.SubmittedByUser)
                    .WithMany()
                    .HasForeignKey(e => e.SubmittedByUserId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Relationship: ApprovedByAdmin
                entity.HasOne(e => e.ApprovedByAdmin)
                    .WithMany()
                    .HasForeignKey(e => e.ApprovedByAdminId)
                    .OnDelete(DeleteBehavior.SetNull);
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

                // Main News relationship
                entity.HasOne(e => e.MainNews)
                    .WithMany(n => n.RelatedNewsAsMain)
                    .HasForeignKey(e => e.MainNewsId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Related News relationship
                entity.HasOne(e => e.RelatedNewsItem)
                    .WithMany(n => n.RelatedNewsAsRelated)
                    .HasForeignKey(e => e.RelatedNewsId)
                    .OnDelete(DeleteBehavior.Restrict);

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

                // Relationships
                entity.HasOne(e => e.News)
                    .WithMany(n => n.Comments)
                    .HasForeignKey(e => e.NewsId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.ParentComment)
                    .WithMany(c => c.ChildComments)
                    .HasForeignKey(e => e.ParentCommentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => e.NewsId);
                entity.HasIndex(e => e.IsApproved);
                entity.HasIndex(e => e.CreatedDate);
            });


            #region User

            // Seed Roles
            var adminRoleId = Guid.NewGuid();
            var editorRoleId = Guid.NewGuid();
            var authorRoleId = Guid.NewGuid();
            var readerRoleId = Guid.NewGuid();

            modelBuilder.Entity<AppRole>().HasData(
                new AppRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Description = "Quản trị viên hệ thống",
                    CreatedDate = DateTime.UtcNow
                },
                new AppRole
                {
                    Id = editorRoleId,
                    Name = "Editor",
                    NormalizedName = "EDITOR",
                    Description = "Biên tập viên, duyệt bài viết",
                    CreatedDate = DateTime.UtcNow
                },
                new AppRole
                {
                    Id = authorRoleId,
                    Name = "Author",
                    NormalizedName = "AUTHOR",
                    Description = "Tác giả, tạo bài viết",
                    CreatedDate = DateTime.UtcNow
                },
                new AppRole
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "READER",
                    Description = "Độc giả, xem bài viết",
                    CreatedDate = DateTime.UtcNow
                }
            );

            // Seed Admin User
            var adminUserId = Guid.NewGuid();
            var hasher = new PasswordHasher<AppUser>();

            var adminUser = new AppUser
            {
                Id = adminUserId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@newspaper.com",
                NormalizedEmail = "ADMIN@NEWSPAPER.COM",
                EmailConfirmed = true,
                FullName = "Administrator",
                Department = "IT",
                IsActive = true,
                CreatedDate = DateTime.UtcNow,
                PhoneNumber = "+84901234567"
            };

            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin@123456");

            modelBuilder.Entity<AppUser>().HasData(adminUser);

            // Assign Admin role to admin user
            modelBuilder.Entity<AppUserRole>().HasData(
                new AppUserRole
                {
                    UserId = adminUserId,
                    RoleId = adminRoleId
                }
            );
            #endregion
        }

    }
}
