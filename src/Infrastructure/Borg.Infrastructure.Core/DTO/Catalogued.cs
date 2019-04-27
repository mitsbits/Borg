﻿using Borg.Infrastructure.Core.DDD.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Borg.Infrastructure.Core.DTO
{
    public abstract class Catalogued : ICatalogued
    {
        public string Key { get; set; }
        public virtual string HumanKey { get; set; }
        public string Value { get; set; }
        public virtual string Hint { get; set; }
        public virtual string Flag { get; set; }

        public override string ToString()
        {
            return $"{Key}|{Value}" + (string.IsNullOrWhiteSpace(Hint) ? string.Empty : $"|{Hint}");
        }
    }
}
