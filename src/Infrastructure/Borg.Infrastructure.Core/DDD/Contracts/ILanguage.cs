﻿namespace Borg.Infrastructure.Core.DDD.Contracts
{
    public interface ILanguage : IHaveTitle
    {
        string TwoLetterISO { get; set; }
    }
}