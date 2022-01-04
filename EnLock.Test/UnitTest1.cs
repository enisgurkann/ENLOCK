using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EnLock.Test;

public class UnitTest1
{
    private readonly TestContext _testContext;

    public UnitTest1()
    {
        _testContext = new TestContext(CreateNewContextOptions());
    }

    private static DbContextOptions<TestContext> CreateNewContextOptions()
    {
        // Create a fresh service provider, and therefore a fresh 
        // InMemory database instance.
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        // Create a new options instance telling the context to use an
        // InMemory database and the new service provider.
        var builder = new DbContextOptionsBuilder<TestContext>();
        builder.UseInMemoryDatabase( "TestDbInMemory")
            .UseInternalServiceProvider(serviceProvider);

        return builder.Options;
    }

    public IEnumerable<TestModel> GetDbSet()
    {
        var list = new List<TestModel>();
        
        list.Add(new TestModel() {Name = "Enis", Surname = "Gürkan"});
        list.Add(new TestModel() {Name = "Enes", Surname = "Gürkan"});
        list.Add(new TestModel() {Name = "Filiz", Surname = "Gürkan"});
        return list;
    }

    [Fact]
    public async Task AddTest()
    {
        using (var context = new TestContext(CreateNewContextOptions()))
        {
            await _testContext.TestDbSet.AddRangeAsync(GetDbSet());
            await _testContext.SaveChangesAsync();
            
            int count = await _testContext.TestDbSet.CountAsync();
            Assert.Equal(count,3);
        }
    }
    
    [Fact]
    public async Task FirstOrDefault_Test()
    {
       using (var context = new TestContext(CreateNewContextOptions()))
       {
           await _testContext.TestDbSet.AddRangeAsync(GetDbSet());
           await _testContext.SaveChangesAsync();
           
           
           var model = await _testContext
               .TestDbSet
               .Where(s=> s.Name == "Enis")
               .ToFirstOrDefaultWithNoLockAsync();
           
           Assert.Equal(model.Name, "Enis");
       }
    }
    
    [Fact]
    public async Task List_Test()
    {
        using (var context = new TestContext(CreateNewContextOptions()))
        {
            await _testContext.TestDbSet.AddRangeAsync(GetDbSet());
            await _testContext.SaveChangesAsync();
           
           
            var list = await _testContext
                .TestDbSet
                .ToListWithNoLockAsync();
           
            Assert.Equal(list.Count(), 3);
        }
    }
    
    [Fact]
    public async Task Any_Test()
    {
        using (var context = new TestContext(CreateNewContextOptions()))
        {
            await _testContext.TestDbSet.AddRangeAsync(GetDbSet());
            await _testContext.SaveChangesAsync();
           
           
            var status = await _testContext
                .TestDbSet
                .Where(s=> s.Name == "Enis")
                .ToAnyWithNoLockAsync();
           
            Assert.Equal(status, true);
        }
    }
    
    [Fact]
    public async Task RemoveTest()
    {
        using (var context = new TestContext(CreateNewContextOptions()))
        {
            await _testContext.TestDbSet.AddRangeAsync(GetDbSet());
            await _testContext.SaveChangesAsync();

            var model = await _testContext.TestDbSet.FirstOrDefaultAsync(s => s.Name == "Enis");
             _testContext.TestDbSet.Remove(model);
            await _testContext.SaveChangesAsync();
            int count = await _testContext.TestDbSet.CountAsync();
            
            Assert.Equal(count, 2);
        }
        
    }
}