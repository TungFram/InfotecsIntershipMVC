using InfotecsIntershipMVC.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfotecsIntershipMVC.DAL.DbContexts
{
    public class InfotecsDBContext : DbContext
    {
        public DbSet<FileEntity> Files { get; set; }
        public DbSet<RecordEntity> Values { get; set; }
        public DbSet<ResultEntity> Results { get; set; }

        public InfotecsDBContext(DbContextOptions options) : base(options)
        {
            /*Database.EnsureDeleted();*/ // Clear all data when start app.
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<FileEntity>()
                .HasKey(f => f.FileID)
                .HasName("PrimaryKey_FileID");*/

            /*modelBuilder.Entity<FileEntity>().Property(f => f.FileID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);*/

            modelBuilder.Entity<RecordEntity>()
                .HasOne(record => record.File)
                .WithMany(file => file.Records)
                .HasForeignKey(record => record.FileID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ResultEntity>()
                .HasOne(result => result.File)
                .WithOne(file => file.Result)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
