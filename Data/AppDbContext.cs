namespace Blackrock_Test.Data
{
    using Blackrock_Test.Modals;
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : DbContext
    {
        public DbSet<RunRecord> RunRecords { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<CreditRating> Ratings { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CreditRating>()
                .HasKey(r => r.Id);

            base.OnModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Portfolio>(entity =>
            {
                entity.HasKey(e => e.Port_ID);
                entity.Property(e => e.Port_Name).IsRequired();
                entity.Property(e => e.Port_Country).IsRequired();
                entity.Property(e => e.Port_CCY).IsRequired();
            });

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne<Portfolio>()
                      .WithMany(p => p.Loans)
                      .HasForeignKey(l => l.PortfolioId);
            });
            modelBuilder.Entity<RunRecord>()
            .HasKey(r => r.Id);

            modelBuilder.Entity<RunRecord>()
                .Property(r => r.RunDate)
                .ValueGeneratedNever();
        }
    }
}
