using Microsoft.EntityFrameworkCore;
using VF.Verify.Domain.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Role> Roles { get; set; }
    public DbSet<Distributor> Distribuitors { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Company> Companies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().ToTable("role");
        modelBuilder.Entity<Distributor>().ToTable("distributor");
        modelBuilder.Entity<Country>().ToTable("country");
        modelBuilder.Entity<Company>().ToTable("company");

        modelBuilder.Entity<Distributor>()
        .Property(d => d.PrimaryColor)
        .HasColumnName("primary_color");

        modelBuilder.Entity<Distributor>()
            .Property(d => d.SecondaryColor)
            .HasColumnName("secondary_color");

        modelBuilder.Entity<Company>()
            .HasOne(c => c.Distributor)
            .WithMany(d => d.Companies)
            .HasForeignKey(c => c.DistributorId);

        modelBuilder.Entity<Company>()
            .Property(d => d.ContactEmail)
            .HasColumnName("contact_email");

        modelBuilder.Entity<Company>()
            .Property(d => d.DistributorId)
            .HasColumnName("distributor_id");

        modelBuilder.Entity<Company>()
            .Property(d => d.ContactFullName)
            .HasColumnName("contact_fullname");

        modelBuilder.Entity<Company>()
            .Property(d => d.IsDistributor)
            .HasColumnName("is_distributor");

        modelBuilder.Entity<Company>()
            .Property(d => d.IsActive)
            .HasColumnName("is_active");

        base.OnModelCreating(modelBuilder);
    }
}
