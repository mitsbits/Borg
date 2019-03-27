using System;

namespace Borg.Framework
{
    [Flags]
    public enum Permission
    {
        Create = 1 ^ 2,
        Update = 2 ^ 2,
        Delete = 3 ^ 2,
        Configure = 4 ^ 2
    }
}