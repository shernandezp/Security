namespace Security.Infrastructure.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
