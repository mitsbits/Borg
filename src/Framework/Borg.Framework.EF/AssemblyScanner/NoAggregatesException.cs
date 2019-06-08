using Borg.Framework.Cms.Annotations;
using System;
using System.Reflection;
using System.Text;

namespace Borg.Framework.EF.AssemblyScanner
{
    internal class NoAggregatesException : ApplicationException
    {
        public NoAggregatesException(Assembly assembly) : base(BuildMessage(assembly))
        {
        }

        private static string BuildMessage(Assembly assembly)
        {
            var builder = new StringBuilder(assembly.FullName);
            builder.AppendLine($"No types are decorated with {nameof(CmsAggregateRootAttribute)} or a subclass");
            return builder.ToString();
        }
    }
}