using Microsoft.EntityFrameworkCore;
using MagazineDomain.Model;

public class IstpContext : DbContext
{
    public IstpContext(DbContextOptions<IstpContext> options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-NGKQJ8A\\SQLEXPRESS01; Database=istp; Trusted_Connection=True; Encrypt=True;TrustServerCertificate=true;  MultipleActiveResultSets=true",
            options => options.EnableRetryOnFailure());
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
        .Property(a => a.AuthorId)
        .ValueGeneratedOnAdd();

        modelBuilder.Entity<Article>()
            .HasOne(a => a.Author)
            .WithMany(b => b.Articles)
            .HasForeignKey(a => a.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public DbSet<MagazineDomain.Model.Author> Author { get; set; } = default!;
    public DbSet<MagazineDomain.Model.Article> Article{ get; set; } = default!;
    public DbSet<MagazineDomain.Model.Editor> Editor { get; set; } = default!;
   public DbSet<MagazineDomain.Model.Magazine> Magazine { get; set; } = default!;

    // DbSet properties for your entities should be defined here
}
