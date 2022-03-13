using EfCoreMinimalReproducibleExample;

using var ctx = new DemoDbContext();

await ctx.Database.EnsureDeletedAsync();
await ctx.Database.EnsureCreatedAsync();

var maintenanceCalculationInformation = new MaintenanceCalculationInformation();
var rootEntity = new RootEntity(maintenanceCalculationInformation);
await ctx.AddAsync(rootEntity);
await ctx.SaveChangesAsync();

using var newCtx = new DemoDbContext();
var loaded = newCtx.RootEntities.First();

Console.WriteLine(loaded.BaseEntity);


