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

    public DbSet<VerificationField> VerificationFields { get; set; }

    public DbSet<Field> Fields { get; set; }

    public DbSet<Profile> Profiles { get; set; }

    public DbSet<ProfileSource> ProfileSources { get; set; }

    public DbSet<ConsultationCriteria> ConsultationCriterias { get; set; }
    public DbSet<CompanySources> CompanySources { get; set; }
    public DbSet<ConsultationCriteriaFields> ConsultationCriteriaFields { get; set; }
    public DbSet<Entity> Entities { get; set; }
    public DbSet<ExtractionMethod> ExtractionMethods { get; set; }
    public DbSet<Source> Sources { get; set; }
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

        modelBuilder.Entity<CompanyCountry>(entity =>
        {
            entity.ToTable("company_country");
            entity.HasKey(cc => cc.Id);
            entity.Property(cc => cc.IsActive).HasColumnName("is_active");
        });


        modelBuilder.Entity<Profile>(entity =>
        {
            entity.ToTable("profile"); // Nombre exacto de la tabla en BD
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Name)
                .HasMaxLength(255)
                .HasColumnName("name"); // Si el nombre en BD es diferente
        });

        modelBuilder.Entity<ProfileSource>(entity =>
        {
            entity.ToTable("profile_source");
            entity.HasKey(ps => ps.Id);

            // Relaciones
            entity.HasOne(ps => ps.Profile)
                .WithMany(p => p.ProfileSources)
                .HasForeignKey(ps => ps.ProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ps => ps.Source)
                .WithMany(s => s.ProfileSources)
                .HasForeignKey(ps => ps.SourceId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ps => ps.ConsultationCriteria)
                .WithMany(cc => cc.ProfileSources)
                .HasForeignKey(ps => ps.ConsultationCriteriaId)
                .OnDelete(DeleteBehavior.SetNull);
        });

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

        modelBuilder.Entity<VerificationField>(entity =>
        {

            entity.ToTable("verification_fields");

            // Clave primaria
            entity.HasKey(vf => vf.Id);

            // Relaciones
            entity.HasOne(vf => vf.ConsultationCriteria)
                .WithMany(cc => cc.VerificationFields)
                .HasForeignKey(vf => vf.ConsultationCriteriaId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(vf => vf.Source)
                .WithMany(s => s.VerificationFields)
                .HasForeignKey(vf => vf.SourceId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(vf => vf.Field)
                .WithMany(f => f.VerificationFields)
                .HasForeignKey(vf => vf.FieldId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configuración para FieldType
        modelBuilder.Entity<Field>(entity =>
        {
            entity.ToTable("field");
            entity.Property(f => f.Type)
                .HasConversion<string>()
                .HasColumnName("type");
        });

        // Configuración para ExtractionMethodType
        modelBuilder.Entity<ExtractionMethod>(entity =>
        {
            entity.ToTable("extraction_method"); // Nombre real de la tabla en BD
            entity.HasKey(em => em.Id);

            entity.Property(em => em.Type)
                .HasConversion<string>() // Convertir enum a string
                .HasColumnName("type")
                .HasMaxLength(10);

            entity.Property(em => em.InternalName)
                .HasColumnName("internal_name")
                .HasMaxLength(255);
        });

        // Configuración para ConsultationCriteria
        modelBuilder.Entity<ConsultationCriteria>(entity =>
        {
            entity.ToTable("ConsultationCriterias"); // Nombre de tabla explícito

            entity.HasKey(cc => cc.Id);

            entity.HasOne(cc => cc.Source)
                .WithMany(s => s.ConsultationCriterias)
                .HasForeignKey(cc => cc.SourceId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(cc => cc.VerificationFields)
                .WithOne(vf => vf.ConsultationCriteria)
                .HasForeignKey(vf => vf.ConsultationCriteriaId);
        });

        // Configuración para VerificationField
        modelBuilder.Entity<VerificationField>(entity =>
        {
            entity.ToTable("VerificationFields");

            entity.HasOne(vf => vf.Field)
                .WithMany(f => f.VerificationFields)
                .HasForeignKey(vf => vf.FieldId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Source>(entity =>
        {
            entity.ToTable("source");
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Name).HasMaxLength(255);
            entity.Property(s => s.BaseUrl).HasColumnName("base_url");
            entity.Property(s => s.IsActive).HasColumnName("is_active");
            entity.HasOne(s => s.ExtractionMethod)
                .WithMany(em => em.Sources)
                .HasForeignKey(s => s.ExtractionMethodId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<CompanySources>(entity =>
        {
            entity.ToTable("company_sources");
            entity.HasKey(cs => cs.Id);

            entity.HasOne(cs => cs.Source)
                .WithMany(s => s.CompanySources)
                .HasForeignKey(cs => cs.SourceId);

            entity.HasOne(cs => cs.CompanyCountry)
                .WithMany(cc => cc.CompanySources)
                .HasForeignKey(cs => cs.CompanyCountryId);
        });

        modelBuilder.Entity<ConsultationCriteriaFields>(entity =>
        {
            entity.ToTable("consultation_criteria_fields");
            entity.HasKey(ccf => ccf.Id);

            entity.HasOne(ccf => ccf.ConsultationCriteria)
                .WithMany(cc => cc.CriteriaFields)
                .HasForeignKey(ccf => ccf.ConsultationCriteriaId);

            entity.HasOne(ccf => ccf.Field)
                .WithMany(f => f.CriteriaFields)
                .HasForeignKey(ccf => ccf.FieldId);
        });

        modelBuilder.Entity<Entity>(entity =>
        {
            entity.ToTable("entity");
            entity.HasOne(e => e.Country)
                .WithMany(c => c.Entities)
                .HasForeignKey(e => e.CountryId);
        });

        modelBuilder.Entity<Source>(entity =>
        {
            entity.ToTable("source");
            entity.HasOne(s => s.Entity)
                .WithMany(e => e.Sources)
                .HasForeignKey(s => s.EntityId);

            entity.HasOne(s => s.ExtractionMethod)
                .WithMany(em => em.Sources)
                .HasForeignKey(s => s.ExtractionMethodId);
        });


        base.OnModelCreating(modelBuilder);
    }
}
