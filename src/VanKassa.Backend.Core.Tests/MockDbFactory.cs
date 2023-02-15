using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using VanKassa.Backend.Infrastructure.Data;

namespace VanKassa.Backend.Core.Tests;

public static class MockDbFactory
{
    public static DbContextOptions<VanKassaDbContext> DbOptions { get; private set; } = null!;
    public static Mock<IDbContextFactory<VanKassaDbContext>> Create()
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
        
        
        return mockDbContextFactory;
    }

    public static void InitMockDbContext(Mock<IDbContextFactory<VanKassaDbContext>> mockDbContextFactory)
        => mockDbContextFactory.Setup(f => f
                .CreateDbContextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new VanKassaDbContext(DbOptions));
}