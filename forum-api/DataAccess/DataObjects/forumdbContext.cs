using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace forum_api.DataAccess.DataObjects
{
    public partial class forumdbContext : DbContext
    {
        public forumdbContext()
        {
        }

        public forumdbContext(DbContextOptions<forumdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Topic> Topics { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=root;database=forum-db", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.25-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => new { e.IdComment, e.TopicIdtopic })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("comment");

                entity.HasIndex(e => e.TopicIdtopic, "fk_comment_topic_idx");

                entity.Property(e => e.IdComment).HasColumnName("id_comment");

                entity.Property(e => e.TopicIdtopic).HasColumnName("topic_idtopic");

                entity.Property(e => e.Content)
                    .HasMaxLength(500)
                    .HasColumnName("content");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_date");

                entity.Property(e => e.ModificationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modification_date");

                entity.Property(e => e.Username)
                    .HasMaxLength(45)
                    .HasColumnName("username");

                //entity.HasOne(d => d.TopicIdtopicNavigation)
                //    .WithMany(p => p.Comments)
                //    .HasForeignKey(d => d.TopicIdtopic)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("fk_comment_topic");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.HasKey(e => e.Idtopic)
                    .HasName("PRIMARY");

                entity.ToTable("topic");

                entity.HasIndex(e => e.Idtopic, "idtopic_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Idtopic).HasColumnName("idtopic");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_date");

                entity.Property(e => e.CreatorName)
                    .HasMaxLength(45)
                    .HasColumnName("creator_name");

                entity.Property(e => e.ModificationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modification_date");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
