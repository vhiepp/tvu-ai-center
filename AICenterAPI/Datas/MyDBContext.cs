using Microsoft.EntityFrameworkCore;

namespace AICenterAPI.Datas
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
        }

        #region DbSet
        public DbSet<User> Users { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsCategory> NewsCategories { get; set; }
        public DbSet<CategoryContent> CategoryContents { get; set; }
        public DbSet<NewsContent> NewsContents { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<NewsCategory>()
                .HasKey(nc => new { nc.NewsId, nc.CategoryId });

            modelBuilder.Entity<CategoryContent>()
                .HasKey(cc => new { cc.CategoryId, cc.Language });

            modelBuilder.Entity<NewsContent>()
                .HasKey(nc => new { nc.NewsId, nc.Language });
        }
    }
}
