using Borg.Framework.EF.Contracts;
using Borg.Infrastructure.Core;
using Borg.Infrastructure.Core.Reflection.Discovery;
using Borg.Infrastructure.Core.Services.Factory;
using Borg.Platform.EF.Instructions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Borg.Framework.EF
{
    public abstract class BorgDbContext<TConfiguration> : BorgDbContext where TConfiguration : IConfiguration
    {
        protected BorgDbContext(ILoggerFactory loggerFactory, TConfiguration configuration, IAssemblyExplorerResult explorerResult) : base(loggerFactory, configuration, explorerResult)
        {
        }

        protected BorgDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        protected BorgDbContext([NotNull] DbContextOptions options, IAssemblyExplorerResult explorerResult) : base(options, explorerResult)
        {
        }
    }

    public abstract partial class BorgDbContext : DbContext
    {
        private IConfiguration configuration;

        public EventHandler<EntityTrackedEventArgs> TrackedEventHandler;
        public EventHandler<EntityStateChangedEventArgs> StateChangedEventHandler;

        protected readonly ILogger Logger;
        protected readonly IAssemblyExplorerResult ExplorerResult;

        private readonly SetUpMode Mode = SetUpMode.None;

        protected BorgDbContext(ILoggerFactory loggerFactory, IConfiguration configuration, IAssemblyExplorerResult explorerResult)
        {
            Logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger(GetType());
            this.configuration = Preconditions.NotNull(configuration, nameof(configuration));
            this.ExplorerResult = Preconditions.NotNull(explorerResult, nameof(explorerResult));
            Mode = SetUpMode.Configuration;
        }

        protected BorgDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }
        protected BorgDbContext([NotNull] DbContextOptions options, Func<BorgDbContextOptions> borgOptionsFactory = null) : base(options)
        {
        }
        protected BorgDbContext([NotNull] DbContextOptions options, IAssemblyExplorerResult explorerResult) : base(options)
        {
            this.ExplorerResult = Preconditions.NotNull(explorerResult, nameof(explorerResult));
        }

        protected BorgDbContext([NotNull] DbContextOptions options, BorgDbContextOptions borgOptions = null) : this(options, () => borgOptions)
        {
            ChangeTracker.Tracked += TrackedEventHandler;
            ChangeTracker.StateChanged += StateChangedEventHandler;
        }

        public virtual string Schema => (Mode == SetUpMode.Configuration) ? CheckOptionsForSchemaName() : GetType().Name.Replace("DbContext", string.Empty).Slugify();

        private BorgDbContextConfiguration BorgOptions { get; set; }
        public bool IsWrappedByUOW { get; set; } = false;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);

            if (Mode == SetUpMode.Configuration)
            {
                SetUpConfig(options);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            Map(builder);
        }

        protected enum SetUpMode
        {
            None,
            Configuration
        }

        #region Private

        private void Map(ModelBuilder builder)
        {
            Debugger.Launch();

            DoYourThnag(ExplorerResult);
            var maptype = typeof(EntityMapBase<,>);
            var maps = GetType().Assembly.GetTypes().Where(t => t.IsSubclassOfRawGeneric(maptype) && !t.IsAbstract && t.BaseType.GenericTypeArguments[1] == GetType());
            var entities = GetType().Assembly.GetTypes().Where(x => x.IsCmsAggregateRoot());
            var havemap = maps.Select(x => x.BaseType.GenericTypeArguments[1]).Distinct().ToArray();
            //var orphans = entities.Where(x => maps.Any(m => m.))

            foreach (var map in maps)
            {
                ((IEntityMap)New.Creator(map)).OnModelCreating(builder);
            }
        }

        private void DoYourThnag(IAssemblyExplorerResult results)
        {
            foreach (var result in results.Results)
            {
                // result.EntityMaps.Select(x => x.GetGenericArguments());
            }
        }

        private string CheckOptionsForSchemaName()
        {
            return (BorgOptions?.Overrides?.Schema ?? string.Empty).IsNullOrWhiteSpace()
              ? GetType().Name.Replace("DbContext", string.Empty).Slugify()
              : BorgOptions?.Overrides?.Schema;
        }

        private void SetUpConfig(DbContextOptionsBuilder options)
        {
            BorgOptions = Configurator<BorgDbContextConfiguration>.Build(Logger, configuration, GetType());
            options.UseSqlServer(BorgOptions.ConnectionString, opt =>
            {
                opt.EnableRetryOnFailure(3, TimeSpan.FromSeconds(30), new int[0]);
                opt.CommandTimeout(BorgOptions.Overrides.CommandTimeout);
            });
            options.EnableDetailedErrors(BorgOptions.Overrides.EnableDetailedErrors);
        }

        #endregion Private
    }
}