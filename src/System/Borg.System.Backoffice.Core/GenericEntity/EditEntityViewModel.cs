using System;
using System.Collections.Generic;
using System.Reflection;

namespace Borg.System.Backoffice.Core.GenericEntity
{
    public abstract class EditEntityViewModel : EntityViewModel
    {
        public abstract object Entity { get; }
        public Dictionary<string, string[]> Tabs { get; }
        public abstract PropertyInfo[] ProperyInfos();
    }

    public class EditEntityViewModel<TEntity> : EditEntityViewModel
    {
        public TEntity Data { get; set; }
        public override object Entity => Data;
        public override Type EntityType => typeof(TEntity);

        public override PropertyInfo[] ProperyInfos() {
            return Data.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
    }

    
}