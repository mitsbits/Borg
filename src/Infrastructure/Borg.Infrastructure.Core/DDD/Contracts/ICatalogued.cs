﻿namespace Borg.Infrastructure.Core.DDD.Contracts
{
    internal interface ICatalogued
    {
        string Key { get; }

        string HumanKey { get; }

        string Value { get; }

        string Hint { get; }

        string Flag { get; }
    }
}