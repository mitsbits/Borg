using Borg.Infrastructure.Core;
using Borg.Infrastructure.Core.DDD.Contracts;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Borg.System.Backoffice.Core.GenericEntity
{
    public class HeaderColumn : GridColumn, IHaveTitle
    {
        public HeaderColumn()
        {
        }

        public HeaderColumn(PropertyInfo property)
        {
            Preconditions.NotNull(property, nameof(property));
            Property = property.Name;
            Title = property.Name.SplitCamelCaseToWords();
            Visible = property.IsSimple();
            IsSimple = property.IsSimple();
        }

        public string Title { get; set; }
        public string Property { get; set; }
        public bool Visible { get; set; }

        public bool IsSimple { get; set; }

        [UIHint("SortDirection")]
        public SortDirection SortDirection { get; set; }
    }
}