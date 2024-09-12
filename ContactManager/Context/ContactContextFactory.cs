using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ContactManager.Context
{
    public class ContactContextFactory : IDesignTimeDbContextFactory<ContactContext>
    {
        public ContactContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ContactContext>();

            // For running migrations
            optionsBuilder.UseNpgsql("Host=localhost;Database=contacts;Username=postgres;Password=asadal");

            return new ContactContext(optionsBuilder.Options);
        }
    }
}
