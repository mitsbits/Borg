using Borg.Infrastructure.Core.DDD.Contracts;

namespace Borg.System.Backoffice.Core.GenericEntity
{
    public class EntityViewModel : IHaveTitle

    {
        public string Title { get; set; }
    }
}