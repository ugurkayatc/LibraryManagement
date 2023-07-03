using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data.Context
{
    public class LibraryManagementDbContext : DbContext
    {
        public LibraryManagementDbContext(DbContextOptions options) : base(options)
        {
        }

        // Represents the "Books" table in the database
        public DbSet<Book> Books { get; set; }

        // Represents the "Users" table in the database
        public DbSet<User> Users { get; set; }

        // Represents the "LentBooks" table in the database
        public DbSet<LentBook> LentBooks { get; set; }

        // Overrides the SaveChangesAsync method to automatically set the CreatedDate and UpdatedDate properties of BaseEntity entities
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = DateTime.UtcNow;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
