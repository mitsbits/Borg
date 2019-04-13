using Borg;
using Borg.Framework.Actors.AntiCorruption;
using Borg.Framework.Actors.GrainContracts;
using Borg.Framework.Actors.Grains;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DIExtensions
    {
        public static IServiceCollection AddSiloCache(this IServiceCollection services)
        {
            var connectionString = "Data Source=.;Initial Catalog=Borg.Actors;User Id=sa;Password=P@ssw0rd;Pooling=False;Max Pool Size=200;MultipleActiveResultSets=True";
            services.AddDbContext<ActorsDbContext>(opt => opt.UseSqlServer(connectionString));
            var builder = new ClientBuilder();

            builder.UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                   {
                       options.ClusterId = "dev";
                       options.ServiceId = "Actors";
                   })
                   .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(ICacheItemGrain<>).Assembly))
                   .ConfigureLogging(logging => logging.AddConsole());

            var client = builder.Build();
            AsyncHelpers.RunSync(() => client.Connect());

            services.AddSingleton<IClusterClient>(client);
            services.AddSingleton<IDistributedCache, SiloCacheProvider>();
            return services;
        }
    }
}