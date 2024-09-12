using ContactManager.Model;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Context
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options)
            : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<Contact> Contacts { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Contact && e.State is EntityState.Added or EntityState.Modified);

            foreach (var entityEntry in entries)
            {
                ((ModelBase)entityEntry.Entity).LastChangeTimestamp = DateTime.Now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Contact>()
                .HasKey(_ => _.Id);

            builder.Entity<Contact>()
                .HasIndex(_ => _.Email)
                .IsUnique();
        }
    }
}
