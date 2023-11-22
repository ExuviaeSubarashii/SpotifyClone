using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SpotifyClone.Domain.Models
{
    public partial class SpotifyCloneContext : DbContext
    {
        public SpotifyCloneContext()
        {
        }

        public SpotifyCloneContext(DbContextOptions<SpotifyCloneContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Playlist> Playlists { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Songs> Songs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-12NGJ7T;Initial Catalog=SpotifyClone;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Playlist>(entity =>
            {
                entity.Property(e => e.PlayListId)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PlayListContents)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PlayListOwner)
                    .HasMaxLength(208)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PlayListType)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.PlayListTitle)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Followers)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Following)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UserName)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UserToken)
                    .HasMaxLength(208)
                    .IsUnicode(false)
                    .IsFixedLength();
            });
            modelBuilder.Entity<Songs>(entity =>
            {
                entity.Property(e => e.SongName)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.DateAdded)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.Duration)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.SongArtist)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.AlbumName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
