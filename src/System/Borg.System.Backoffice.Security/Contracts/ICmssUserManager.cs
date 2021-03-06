﻿using Borg.Framework.Cms.Contracts;
using System.Threading.Tasks;

namespace Borg.System.Backoffice.Security.Contracts
{
    public interface ICmsUserManager<TData>
    {
        Task<ICmsOperationResult<TData>> Login(string user, string password);

        Task<ICmsOperationResult> SetPassword(string user, string password);
    }
}