namespace Borg.Framework.EF.Contracts
{
    public interface IUOWDbContext
    {
        string Schema { get; }
    }

    public interface IBorgDbContextOptions
    {
        string OverrideSchema { get; }
    }

    public class BorgDbContextOptions : IBorgDbContextOptions
    {
        public string OverrideSchema { get; set; } = string.Empty;
    }
}