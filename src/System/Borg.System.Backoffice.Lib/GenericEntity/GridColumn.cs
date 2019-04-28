using Borg.Infrastructure.Core.DDD.Contracts;

namespace Borg.System.Backoffice.Lib
{
    public class GridColumn : IHaveOrder
    {
        public int Order { get; set; }
    }
}