﻿namespace Borg.Framework.EF
{
    public class BorgDbContextConfiguration
    {
        public string ConnectionString { get; set; }
        public DbContextOverrides Overrides { get; set; }
    }

    public class DbContextOverrides
    {
        private const string DefaultBorgSchema = "borg";
        public string Schema { get; set; } = DefaultBorgSchema;
        public string TablePrefix { get; set; } = string.Empty;
        public int CommandTimeout { get; set; } = 30;
        public bool EnableDetailedErrors { get; set; } = false;
    }
}