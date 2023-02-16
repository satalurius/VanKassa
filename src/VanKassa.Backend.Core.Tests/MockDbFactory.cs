using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using VanKassa.Backend.Infrastructure.Data;

namespace VanKassa.Backend.Core.Tests;

public class MockDbFactory
{
    public DbContextOptions<VanKassaDbContext> DbOptions { get; private set; } = null!;

    public Mock<IDbContextFactory<VanKassaDbContext>> Create()
    {
        Mock<IDbContextFactory<VanKassaDbContext>> mockDbContextFactory = new();
        DbOptions = new DbContextOptionsBuilder<VanKassaDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        mockDbContextFactory.Setup(f =>
                f.CreateDbContext())
            .Returns(new VanKassaDbContext(
                    DbOptions
                )
            );

        mockDbContextFactory.Setup(f => 
                f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new VanKassaDbContext(DbOptions));

        return mockDbContextFactory;
    }
}