using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MagazineDomain.Model;

public partial class IstpContext : DbContext
{
    public IstpContext()
    {
    }

    public IstpContext(DbContextOptions<IstpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Editor> Editors { get; set; }

    public virtual DbSet<Magazine> Magazines { get; set; }

    public virtual DbSet<Reader> Readers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-NGKQJ8A\\SQLEXPRESS01; Database=istp; Trusted_Connection=True; TrustServerCertificate=True; ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.ArticleId).HasName("PK__Article__9C6270C822E0790F");

            entity.ToTable("Article");

            entity.Property(e => e.ArticleId)
                .ValueGeneratedNever()
                .HasColumnName("ArticleID");
            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.EditorId).HasColumnName("EditorID");
            entity.Property(e => e.MagazineId).HasColumnName("MagazineID");
            entity.Property(e => e.TextContent).HasColumnType("text");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Author).WithMany(p => p.Articles)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__Article__AuthorI__3F466844");

            entity.HasOne(d => d.Editor).WithMany(p => p.Articles)
                .HasForeignKey(d => d.EditorId)
                .HasConstraintName("FK__Article__EditorI__403A8C7D");

            entity.HasOne(d => d.Magazine).WithMany(p => p.Articles)
                .HasForeignKey(d => d.MagazineId)
                .HasConstraintName("FK__Article__Magazin__412EB0B6");
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Author__70DAFC14BB245FBA");

            entity.ToTable("Author");

            entity.Property(e => e.AuthorId)
                .ValueGeneratedNever()
                .HasColumnName("AuthorID");
            entity.Property(e => e.AuthorName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment__C3B4DFAA851D1FB9");

            entity.ToTable("Comment");

            entity.Property(e => e.CommentId)
                .ValueGeneratedNever()
                .HasColumnName("CommentID");
            entity.Property(e => e.ArticleId).HasColumnName("ArticleID");
            entity.Property(e => e.CommentAuthorId).HasColumnName("CommentAuthorID");
            entity.Property(e => e.CommentText).HasColumnType("text");

            entity.HasOne(d => d.Article).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ArticleId)
                .HasConstraintName("FK__Comment__Article__440B1D61");

            entity.HasOne(d => d.CommentAuthor).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CommentAuthorId)
                .HasConstraintName("FK__Comment__Comment__44FF419A");
        });

        modelBuilder.Entity<Editor>(entity =>
        {
            entity.HasKey(e => e.EditorId).HasName("PK__Editor__986DCA2AB980DFA6");

            entity.ToTable("Editor");

            entity.Property(e => e.EditorId)
                .ValueGeneratedNever()
                .HasColumnName("EditorID");
            entity.Property(e => e.EditorName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Magazine>(entity =>
        {
            entity.HasKey(e => e.MagazineId).HasName("PK__Magazine__9C55D06A11C4A31A");

            entity.ToTable("Magazine");

            entity.Property(e => e.MagazineId)
                .ValueGeneratedNever()
                .HasColumnName("MagazineID");
            entity.Property(e => e.MagazineName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Reader>(entity =>
        {
            entity.HasKey(e => e.ReaderId).HasName("PK__Reader__8E67A581B5261E94");

            entity.ToTable("Reader");

            entity.Property(e => e.ReaderId)
                .ValueGeneratedNever()
                .HasColumnName("ReaderID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ReaderName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
