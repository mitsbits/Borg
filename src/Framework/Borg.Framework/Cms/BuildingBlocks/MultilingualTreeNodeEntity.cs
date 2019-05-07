using Borg.Infrastructure.Core.DDD.Contracts;
using System;

namespace Borg.Framework.Cms.BuildingBlocks
{
    public abstract class MultilingualTreeNodeEntity<TKey> : MultilingualEntity<TKey>, ITreeNode<TKey> where TKey : IEquatable<TKey>
    {
        public abstract TKey ParentId { get; set; }
        public virtual int Depth { get; set; }

        public virtual bool IsRoot() => ParentId.Equals(default);
    }
}