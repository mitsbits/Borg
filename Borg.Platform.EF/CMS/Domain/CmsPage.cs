using Borg.Framework.Cms.Annotations;
using Borg.Framework.Cms.BuildingBlocks;
using Borg.Framework.EF.Instructions.Attributes;
using Borg.Infrastructure.Core.DDD.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Borg.Platform.EF.CMS.Domain
{
    [CmsEntity(Plural = "Menus", Singular = "Menu")]
    [KeySequenceDefinition]
    public class CmsPage : MultilingualEntity<int, CmsLanguage>, IHaveTitle
    {
        public string Title { get; set; }

    }
}
