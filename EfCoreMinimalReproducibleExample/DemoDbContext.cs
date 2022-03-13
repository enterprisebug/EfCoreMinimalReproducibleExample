using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreMinimalReproducibleExample
{
    public class DemoDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer("Server=127.0.0.1,5433;Initial Catalog=DemoDbContextTable;User Id=sa;Password=Pass@word;MultipleActiveResultSets=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CalculationInformation>(b =>
            {
                b.ToTable("BaseEntities");
                b
                .HasDiscriminator<string>("Discriminator")
                .HasValue<ServiceContractCalculationInformation>("ServiceContractCalculationInformationDiscriminator")
                .HasValue<MaintenanceCalculationInformation>("MaintenanceCalculationInformationDiscriminator");
            });

            modelBuilder.Entity<ServiceContractCalculationInformation>(b =>
            {
                b.OwnsOne(x => x.OperatingHourRange, ob =>
                {
                    ob.Property(x => x!.Start).HasConversion(ValueTypeConverters.OperatingHoursValueConverter);
                    ob.Property(x => x!.End).HasConversion(ValueTypeConverters.OperatingHoursValueConverter);
                });
                b.Property(x => x.RunTimePerYearAndEngine).HasConversion(ValueTypeConverters.OperatingHoursValueConverter);
            });
        }

        public DbSet<CalculationInformation> CalculationInformations => Set<CalculationInformation>();
        public DbSet<ServiceContractCalculationInformation> DerivedEntity1s => Set<ServiceContractCalculationInformation>();
        public DbSet<MaintenanceCalculationInformation> MaintenanceCalculationInformations => Set<MaintenanceCalculationInformation>();
        public DbSet<RootEntity> RootEntities => Set<RootEntity>();
    }

    public static class ValueTypeConverters
    {
        public static ValueConverter<OperatingHours, int> OperatingHoursValueConverter { get; } = new(
            v => v.Value,
            v => new OperatingHours(v)
        );
    }

    public class RootEntity {

        protected RootEntity() => (BaseEntity) = (null!);

        public RootEntity(CalculationInformation baseEntity)
        {
            BaseEntity = baseEntity;
        }

        public Guid Id { get; set; }
        public virtual CalculationInformation BaseEntity { get; private set; }
    }

    public abstract class CalculationInformation
    {
        protected CalculationInformation() { }
        public Guid Id { get; private  set; }
        internal abstract OperatingHours OverallOperatingHours { get; }
    }

    public class ServiceContractCalculationInformation : CalculationInformation
    {
        protected ServiceContractCalculationInformation() => (OperatingHourRange, RunTimePerYearAndEngine) = (null!, null!);

        internal ServiceContractCalculationInformation(OperatingHourRange operatingHourRange, OperatingHours runTimePerYearAndEngine) : base()
        {
            OperatingHourRange = operatingHourRange;
            RunTimePerYearAndEngine = runTimePerYearAndEngine;
        }
        public OperatingHourRange OperatingHourRange { get; private set; }
        public OperatingHours RunTimePerYearAndEngine { get; private set; }

        internal override OperatingHours OverallOperatingHours => OperatingHourRange.OverallOperatingHours;
    }

    public class MaintenanceCalculationInformation : CalculationInformation
    {
        protected internal MaintenanceCalculationInformation() : base()
        {

        }
        internal override OperatingHours OverallOperatingHours => OperatingHours.Zero;
    }
}

public record OperatingHours
{
    public OperatingHours(int value)
    {
        Value = value;
    }

    public static OperatingHours Zero => new(0);

    public int Value { get; }

#pragma warning disable CA1062 // Validate arguments of public methods
    public static implicit operator int(OperatingHours valueType) => valueType.Value;
    public static OperatingHours operator -(OperatingHours left, OperatingHours right) => new(left.Value - right.Value);
    public static OperatingHours operator +(OperatingHours left, OperatingHours right) => new(left.Value + right.Value);
#pragma warning restore CA1062 // Validate arguments of public methods
}

public record OperatingHourRange
{
    //protected OperatingHourRange() => (Start, End) = (null!, null!);
    public OperatingHourRange(OperatingHours start, OperatingHours end)
    {
        Start = start ?? throw new ArgumentNullException(nameof(start));
        End = end ?? throw new ArgumentNullException(nameof(end));
    }
    public OperatingHours Start { get; private init; }
    public OperatingHours End { get; private init; }
    public OperatingHours OverallOperatingHours => End - Start;
}