using Borg.Framework.Cms.BuildingBlocks;

namespace Borg.Platform.EF.DMS.Domain
{
    [PlatformDBAggregateRoot(Plural = "Documents", Singular = "Document")]
    public class Document : DocumentBase<int>
    {
    }
}