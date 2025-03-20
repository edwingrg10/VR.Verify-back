using Microsoft.EntityFrameworkCore;
using VF.Verify.Domain.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Distributor> Distribuitors { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<CompanyCountry> CompanyCountries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().ToTable("role");
        modelBuilder.Entity<Distributor>().ToTable("distributor");
        modelBuilder.Entity<Country>().ToTable("country");
        modelBuilder.Entity<Company>().ToTable("company");
        modelBuilder.Entity<CompanyCountry>().ToTable("company_country");

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

        modelBuilder.Entity<CompanyCountry>()
            .HasOne(cc => cc.Company)
            .WithMany(c => c.CompanyCountries)
            .HasForeignKey(cc => cc.CompanyId);

        modelBuilder.Entity<CompanyCountry>()
            .HasOne(cc => cc.Country)
            .WithMany(c => c.CompanyCountries)
            .HasForeignKey(cc => cc.CountryId);

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("user");

            entity.HasKey(u => u.Id);

            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(255);

            // Relaciones con llaves foraneas
            entity.HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RolId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(u => u.Distributor)
                .WithMany()
                .HasForeignKey(u => u.DistributorId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(u => u.CompanyCountry)
                .WithMany()
                .HasForeignKey(u => u.CompanyCountryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        base.OnModelCreating(modelBuilder);
    }
}
