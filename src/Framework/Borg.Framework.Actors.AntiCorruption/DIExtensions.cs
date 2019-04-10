using Borg.Framework.Actors.AntiCorruption;
using Borg;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Borg.Framework.Actors.GrainContracts;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DIExtensions
    {
        public static IServiceCollection AddSiloCache(this IServiceCollection services)
        {
            var builder = new ClientBuilder();

            builder.UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                   {
                       options.ClusterId = "dev";
                       options.ServiceId = "Actors";
                   })
                   .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(ICacheItemGrain).Assembly))
                   .ConfigureLogging(logging => logging.AddConsole());
            var client = builder.Build();
            AsyncHelpers.RunSync(() => client.Connect());

            services.AddSingleton<IClusterClient>(client);
            services.AddSingleton<IDistributedCache, SiloCacheProvider>();
            return services;
        }
    }
}
