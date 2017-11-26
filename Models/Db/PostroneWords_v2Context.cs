using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PostponedWords.Models.Db
{
    public partial class PostroneWords_v2Context : DbContext
    {
        public virtual DbSet<Dictionary> Dictionary { get; set; }
        public virtual DbSet<DictionaryList> DictionaryList { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Word> Word { get; set; }

		public PostroneWords_v2Context(DbContextOptions<PostroneWords_v2Context> options)
		: base(options)
		{ }
		

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dictionary>(entity =>
            {
                entity.Property(e => e.WordAddDate).HasColumnType("date");
            });

            modelBuilder.Entity<DictionaryList>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("char(50)");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.Telegram).HasColumnType("nchar(50)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("nchar(50)");

                entity.Property(e => e.RegistrationDate).HasColumnType("date");
            });

            modelBuilder.Entity<Word>(entity =>
            {
                entity.Property(e => e.Example)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Meaning)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.WordText)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
