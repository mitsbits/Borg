using Borg.Framework.DAL;
using Borg.Infrastructure.Core.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Borg.System.Backoffice.Core.GenericEntity
{
    public abstract class EntityGridViewModel : EntityViewModel
    {
        public string ReorderColumns { get; set; } = string.Empty;
        public abstract IPagedResult<object> Pager { get; }
        public IList<HeaderColumn> HeaderColumns { get; set; }
        public override DmlOperation DmlOperation { get => DmlOperation.Query; set => base.DmlOperation = DmlOperation.Query; }
    }

    public class EntityGridViewModel<TEntity> : EntityGridViewModel
    {
        public IPagedResult<TEntity> Data { get; set; }
        public override IPagedResult<object> Pager => (Data != null && Data.Records != null && Data.Records.Any()) ? new PagedResult<object>(Data.Records.Cast<object>(), Data.Page, Data.PageSize, Data.TotalRecords) : new PagedResult<object>();
        public override Type EntityType => typeof(TEntity);
    }

    public abstract class EditEntityViewModel : EntityViewModel
    {
        public abstract object Entity { get; }
    }

    public class EditEntityViewModel<TEntity> : EditEntityViewModel
    {
        public TEntity Data { get; set; }
        public override object Entity => Data;
        public override Type EntityType => typeof(TEntity);
    }
}