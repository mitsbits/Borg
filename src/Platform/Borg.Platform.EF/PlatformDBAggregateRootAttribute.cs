using Borg.Framework.EF.Instructions.Attributes;

namespace Borg.Platform.EF
{
    public class PlatformDBAggregateRootAttribute : EFAggregateRootAttribute
    {
        public PlatformDBAggregateRootAttribute() : base(typeof(BorgDb))
        {
        }
    }
}