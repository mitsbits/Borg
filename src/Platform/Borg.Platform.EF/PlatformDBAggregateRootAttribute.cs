using Borg.Framework.EF.Instructions.Attributes;
using System;

namespace Borg.Platform.EF
{
    public class PlatformDBAggregateRootAttribute : EFAggregateRootAttribute
    {
        public override Type DbType { get => typeof(PlatformID); set => throw new NotImplementedException(); }
    }
}