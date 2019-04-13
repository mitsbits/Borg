﻿using Borg.Framework.Actors.Grains;

using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Providers.EntityFramework.Extensions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Borg.Framwork.Actors.Silos
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                var host = await StartSilo();
                Console.WriteLine("Press Enter to terminate...");
                Console.ReadLine();

                await host.StopAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static async Task<ISiloHost> StartSilo()
        {
            var invariant = "System.Data.SqlClient"; // for Microsoft SQL Server
            var connectionString = "Data Source=.;Initial Catalog=Borg.Actors;User Id=sa;Password=P@ssw0rd;Pooling=False;Max Pool Size=200;MultipleActiveResultSets=True";

            var builder = new SiloHostBuilder();
            // Use localhost clustering for a single local silo
            builder.UseLocalhostClustering()
            // Configure ClusterId and ServiceId
            .Configure<ClusterOptions>(options =>
            {
                options.ClusterId = "dev";
                options.ServiceId = "Actors";
            })
        // Configure connectivity
        .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
        .AddAzureTableGrainStorage("TableStore", options => options.ConnectionString = "UseDevelopmentStorage=true")
        .UseAzureTableReminderService(options => options.ConnectionString = "UseDevelopmentStorage=true")
            // Configure logging with any logging framework that supports Microsoft.Extensions.Logging.
            // In this particular case it logs using the Microsoft.Extensions.Logging.Console package.
            .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(CacheItemGrain).Assembly).WithReferences())
        .UseAdoNetClustering(options =>
        {
            options.Invariant = invariant;
            options.ConnectionString = connectionString;
        });
            //use AdoNet for reminder service
            builder.UseAdoNetReminderService(options =>
            {
                options.Invariant = invariant;
                options.ConnectionString = connectionString;
            });

            builder.AddAdoNetGrainStorage("Actors", options =>
            {
                options.Invariant = invariant;
                options.ConnectionString = connectionString;
                options.UseJsonFormat = true;
                options.IndentJson = true;
            })
            .ConfigureLogging(logging => logging.AddConsole());

            builder.AddEfGrainStorage<ActorsDbContext>("ef");

            var host = builder.Build();
         
            await host.StartAsync();
            return host;
        }
    }
}