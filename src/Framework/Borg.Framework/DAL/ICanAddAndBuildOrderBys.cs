namespace Borg.Framework.DAL
{
    public interface ICanAddAndBuildOrderBys<T> : ICanAddOrderBys<T>, ICanProduceOrderBys<T> where T : class
    {
    }
}