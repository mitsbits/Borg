using Borg.Infrastructure.Core.DDD.Contracts;
using System;

namespace Borg.Framework.Cms.BuildingBlocks
{
    public abstract class TagBase<TKey> : MultilingualEntity<TKey>, ITag where TKey : IEquatable<TKey>
    {
        public virtual string Title { get; set; }
        public virtual string Slug { get; set; }
        public virtual bool IsActive { get; set; }
    }
}