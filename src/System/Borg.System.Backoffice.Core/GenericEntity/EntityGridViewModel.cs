using Borg.Infrastructure.Core.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Borg.System.Backoffice.Core.GenericEntity
{
    public abstract class EntityGridViewModel : EntityViewModel
    {
        public string ReorderColumns { get; set; } = string.Empty;
        public abstract IPagedResult<object> PagerData { get; }
        public abstract Type EntityType { get; }
        public IList<HeaderColumn> HeaderColumns { get; set; }
    }

    public class EntityGridViewModel<TEntity> : EntityGridViewModel
    {
        public IPagedResult<TEntity> Data { get; set; }
        public override IPagedResult<object> PagerData => (Data != null && Data.Records != null && Data.Records.Any()) ? new PagedResult<object>(Data.Records.Cast<object>(), Data.Page, Data.PageSize, Data.TotalRecords) : new PagedResult<object>();

        public override Type EntityType => typeof(TEntity);
    }
}