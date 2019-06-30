using Borg.Framework.DAL;
using Borg.Infrastructure.Core.DDD.Contracts;
using System;

namespace Borg.System.Backoffice.Core.GenericEntity
{
    public class EntityViewModel : IHaveTitle

    {
        public virtual DmlOperation DmlOperation { get; set; }
        public string Title { get; set; }
        public virtual Type EntityType { get; }
    }
}