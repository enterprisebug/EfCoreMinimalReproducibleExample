using EfCoreMinimalReproducibleExample;

using var ctx = new DemoDbContext();

await ctx.Database.EnsureDeletedAsync();
await ctx.Database.EnsureCreatedAsync();

Console.WriteLine("herp");