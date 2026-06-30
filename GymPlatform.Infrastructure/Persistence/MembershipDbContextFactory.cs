using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GymPlatform.Infrastructure.Persistence;

public class MembershipDbContextFactory : IDesignTimeDbContextFactory<MembershipDbContext>
{
    public MembershipDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MembershipDbContext>();
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\mssqllocaldb;Database=GymPlatform;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");

        return new MembershipDbContext(optionsBuilder.Options);
    }
}