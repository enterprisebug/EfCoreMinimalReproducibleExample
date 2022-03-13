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
//var loadDerivedEntity1 = newCtx.DerivedEntity1s.First();
var loadDerivedEntity2 = newCtx.RootEntities.First();

//Console.WriteLine(loadDerivedEntity1);
Console.WriteLine(loadDerivedEntity2);

Console.WriteLine("Loaded");

