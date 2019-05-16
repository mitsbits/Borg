using Borg.Framework.DAL;
using Borg.Framework.EF.Contracts;
using Borg.Infrastructure.Core.Services.Factory;
using Borg.Platform.EF.Instructions;

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Borg.Framework.EF
{
    public abstract class BorgDbContext : DbContext
    {
        private bool enableOnConfiguring;
        private IConfiguration configuration;

        protected BorgDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            enableOnConfiguring = true;
        }

        protected BorgDbContext([NotNull] DbContextOptions options, Func<BorgDbContextOptions> borgOptionsFactory = null) : base(options)
        {
            BorgOptions = borgOptionsFactory == null ? new BorgDbContextOptions() : borgOptionsFactory();
        }

        protected BorgDbContext([NotNull] DbContextOptions options, BorgDbContextOptions borgOptions = null) : this(options, () => borgOptions)
        {
            ChangeTracker.Tracked += OnEntityTracked;
            ChangeTracker.StateChanged += OnEntityStateChanged;
        }

        public virtual string Schema => BorgOptions.OverrideSchema.IsNullOrWhiteSpace()
            ? GetType().Name.Replace("DbContext", string.Empty).Slugify()
            : BorgOptions.OverrideSchema.Slugify();

        protected IBorgDbContextOptions BorgOptions { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
            if (enableOnConfiguring)
            {
                options.UseSqlServer(configuration[$"{GetType().Name}:ConnectionString"], opt =>
                {
                    opt.EnableRetryOnFailure(3, TimeSpan.FromSeconds(30), new int[0]);
                    var config = configuration.GetSection($"{GetType().Name}:Configuration").Get<BorgDbContextConfiguration>();
                    opt.CommandTimeout(config.CommandTimeout);
                });
                // options.UseLoggerFactory(loggerFactory).EnableDetailedErrors(environment.IsDevelopment()).EnableSensitiveDataLogging(environment.IsDevelopment());
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            Map(builder);
        }

        #region Private

        private void Map(ModelBuilder builder)
        {
            var maptype = typeof(EntityMap<,>);
            var maps = GetType().Assembly.GetTypes().Where(t => t.IsSubclassOfRawGeneric(maptype) && !t.IsAbstract && t.BaseType.GenericTypeArguments[1] == GetType());
            foreach (var map in maps)
            {
                ((IEntityMap)New.Creator(map)).OnModelCreating(builder);
            }
        }

        private void OnEntityTracked(object sender, EntityTrackedEventArgs e)
        {
        }

        private void OnEntityStateChanged(object sender, EntityStateChangedEventArgs e)
        {
        }

        #endregion Private
    }
}