using InfotecsIntershipMVC.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace InfotecsIntershipMVC.DAL.DbContexts
{
    public class InfotecsDBContext : DbContext
    {
        public DbSet<FileEntity> Files { get; set; }
        public DbSet<RecordEntity> Values { get; set; }
        public DbSet<ResultEntity> Results { get; set; }

        public InfotecsDBContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecordEntity>()
                .HasOne<FileEntity>()
                .WithMany(file => file.Records)
                .HasForeignKey(record => record.FileID);
        }

    }
}
