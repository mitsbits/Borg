using System;
using System.Collections.Generic;
using System.Text;

namespace Borg.Framework.Azure.Storage.Tables
{
    public interface IHaveTableKey
    {
        AzureTableCompositeKey Key { get; }
    }
}
