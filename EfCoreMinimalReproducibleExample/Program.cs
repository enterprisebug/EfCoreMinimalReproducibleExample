using EfCoreMinimalReproducibleExample;

using var ctx = new DemoDbContext();

await ctx.Database.EnsureDeletedAsync();
await ctx.Database.EnsureCreatedAsync();

var derivedEntity2 = new MaintenanceCalculationInformation();
var rootEntity = new RootEntity(derivedEntity2);
await ctx.AddAsync(rootEntity);
await ctx.SaveChangesAsync();

Console.WriteLine("Seeded"); 

using var newCtx = new DemoDbContext();
var loaded = newCtx.RootEntities.First();
Console.WriteLine(loaded.BaseEntity);

Console.WriteLine("Loaded");

