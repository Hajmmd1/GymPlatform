using GymPlatform.Modules.Communication.Domain.Entities;
using GymPlatform.Modules.Communication.Domain.Repositories;
using GymPlatform.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace GymPlatform.Infrastructure.Persistence;

public sealed class CommunicationDbContext : DbContext, ICommunicationUnitOfWork, IUnitOfWork
{
    public CommunicationDbContext(DbContextOptions<CommunicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Session> Sessions => Set<Session>();

    public DbSet<Booking> Bookings => Set<Booking>();

    public DbSet<Room> Rooms => Set<Room>();

    public DbSet<CoachAvailability> CoachAvailabilities => Set<CoachAvailability>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommunicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
