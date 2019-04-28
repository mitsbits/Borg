using Borg.Infrastructure.Core.DDD.Contracts;

namespace Borg.System.Backoffice.Lib
{
    public class EntityViewModel : IHaveTitle

    {
        public string Title { get; set; }
    }
}