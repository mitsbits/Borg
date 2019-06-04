using Borg.Framework.Cms.Annotations;
using Borg.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;
using System;

namespace Borg.Framework.EF.Instructions.Attributes
{
    public class EFAggregateRootAttribute : CmsAggregateRootAttribute
    {
        private Type dbType;

        public virtual Type DbType
        {
            get { return dbType; }
            set
            {
                var db = Preconditions.NotNull(value, nameof(value));
                if (db.IsSubclassOf(typeof(DbContext)))
                {
                    DbType = db;
                }
                throw new NotSubclassOfException(value, typeof(DbContext));
            }
        }
    }
}