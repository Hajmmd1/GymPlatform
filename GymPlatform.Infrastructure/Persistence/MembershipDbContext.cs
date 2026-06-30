using GymPlatform.Modules.Membership.Domain.Entities;
using GymPlatform.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace GymPlatform.Infrastructure.Persistence;

public sealed class MembershipDbContext : DbContext, IUnitOfWork
{
    public MembershipDbContext(DbContextOptions<MembershipDbContext> options)
        : base(options)
    {
    }

    public DbSet<Gym> Gyms => Set<Gym>();

    public DbSet<Member> Members => Set<Member>();

    public DbSet<Coach> Coaches => Set<Coach>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MembershipDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
