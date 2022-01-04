using Microsoft.EntityFrameworkCore;

namespace EnLock.Test;

public class TestContext: DbContext
{
    public TestContext() { }
    public TestContext(DbContextOptions<TestContext> options): base(options) { }
    public DbSet<TestModel> TestDbSet { get; set; }
}

