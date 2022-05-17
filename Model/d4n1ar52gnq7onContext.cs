using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace petdiary.Model
{
    public partial class d4n1ar52gnq7onContext : DbContext
    {
        public d4n1ar52gnq7onContext()
        {
        }

        public d4n1ar52gnq7onContext(DbContextOptions<d4n1ar52gnq7onContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Board> Boards { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Diary> Diaries { get; set; }
        public virtual DbSet<Title> Titles { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=ec2-44-195-16-34.compute-1.amazonaws.com;Database=d4n1ar52gnq7on;Port=5432;sslmode=Require;User Id=reqptsrojxundf;Password=08a6e4e0c8c8467aae8213984749755c00dfec10ce2942dc98729b60216a2e5f;Trust Server Certificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.UTF-8");

            modelBuilder.Entity<Board>(entity =>
            {
                entity.ToTable("board");

                entity.Property(e => e.Boardid)
                    .HasColumnName("boardid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AddDate).HasColumnName("addDate");

                entity.Property(e => e.Detail)
                    .IsRequired()
                    .HasColumnName("detail");

                entity.Property(e => e.IsShow).HasColumnName("isShow");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("title");

                entity.Property(e => e.Typeid).HasColumnName("typeid");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Boards)
                    .HasForeignKey(d => d.Typeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_type");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comment");

                entity.HasIndex(e => e.Userid, "fki_co");

                entity.Property(e => e.Commentid)
                    .HasColumnName("commentid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AddDate).HasColumnName("addDate");

                entity.Property(e => e.Boardid).HasColumnName("boardid");

                entity.Property(e => e.IsOwner).HasColumnName("isOwner");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("path");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnName("text");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Board)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.Boardid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_board");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user");
            });

            modelBuilder.Entity<Diary>(entity =>
            {
                entity.ToTable("diary");

                entity.Property(e => e.Diaryid)
                    .HasColumnName("diaryid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AddDate).HasColumnName("addDate");

                entity.Property(e => e.Detail)
                    .IsRequired()
                    .HasColumnName("detail");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("path");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("title");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Diaries)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user");
            });

            modelBuilder.Entity<Title>(entity =>
            {
                entity.ToTable("title");

                entity.Property(e => e.Titleid)
                    .HasColumnName("titleid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.ToTable("type");

                entity.Property(e => e.Typeid)
                    .HasColumnName("typeid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.NameEng)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nameEng");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Titleid, "fki_title");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Alias)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("alias");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("firstname");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lastname");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("password");

                entity.Property(e => e.RegisDate).HasColumnName("regisDate");

                entity.Property(e => e.Titleid).HasColumnName("titleid");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("username");

                entity.HasOne(d => d.Title)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Titleid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("title");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
