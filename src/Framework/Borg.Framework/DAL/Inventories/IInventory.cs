namespace Borg.Framework.DAL.Inventories
{
    public interface IInventory
    {
        bool SupportsReads { get; }
        bool SupportsWrites { get; }
        bool SupportsQueries { get; }
        bool SupportsQuerable { get; }
        bool SupportsProjections { get; }
        bool SupportsSearch { get; }
    }

    public interface IInventory<T> : IInventory
    {
    }

    public interface IReadInventory<T> : IInventory<T>, IReadRepository<T> where T : class
    {
    }

    public interface IWriteInventory<T> : IInventory<T>, IWriteRepository<T> where T : class
    {
    }

    public interface IQuerableInventory<T> : IInventory<T>, IQuerableRepository<T> where T : class
    {
    }
    public interface IQueryInventory<T> : IInventory<T>, IQueryRepository<T> where T : class
    {
    }

    public interface ISearchInventory<T> : IInventory<T>, ISearchRepository<T> where T : class
    {
    }

    public interface IProjectionInventory<T, TProjection>
    {
    }
}