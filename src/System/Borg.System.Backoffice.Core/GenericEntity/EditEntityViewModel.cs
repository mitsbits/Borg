using System;
using System.Collections.Generic;

namespace Borg.System.Backoffice.Core.GenericEntity
{
    public abstract class EditEntityViewModel : EntityViewModel
    {
        public abstract object Entity { get; }
        public Dictionary<string, string[]> Tabs { get; }
    }

    public class EditEntityViewModel<TEntity> : EditEntityViewModel
    {
        public TEntity Data { get; set; }
        public override object Entity => Data;
        public override Type EntityType => typeof(TEntity);
    }
}