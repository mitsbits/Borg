using Borg.Infrastructure.Core.DDD.Contracts;

namespace Borg.System.Backoffice.Core.GenericEntity
{
    public class GridColumn : IHaveOrder
    {
        public int Order { get; set; }
    }
}