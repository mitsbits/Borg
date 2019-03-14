using System;
using System.Collections.Generic;
using System.Text;
using Borg.Framework.MVC.UI;
using Microsoft.AspNetCore.Hosting;

namespace Borg.System.Backoffice
{
    public sealed class UiConfigureOptions : BaseModuleUiConfigureOptions
    {
        public UiConfigureOptions(IHostingEnvironment environment) : base(environment)
        {
        }
    }
}
