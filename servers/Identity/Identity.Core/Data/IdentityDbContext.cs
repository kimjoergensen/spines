namespace Identity.Core.Data;

using Identity.Core.Models;

using Microsoft.EntityFrameworkCore;

public class IdentityDbContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext
{
    public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("Identity");
        base.OnModelCreating(builder);
    }
}
