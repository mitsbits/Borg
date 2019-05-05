using Borg.Infrastructure.Core;
using Borg.Infrastructure.Core.DDD;
using Borg.Infrastructure.Core.DDD.Contracts;
using Borg.Infrastructure.Core.DI;
using Borg.Infrastructure.Core.Services.Multilingual;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Borg.Framework.Services.Multilingual
{
    [PlugableService(ImplementationOf = typeof(ILanguageOptionsProvider), Lifetime = Lifetime.Singleton, OneOfMany = true, Order = -999)]
    public class LanguageOptionsProvider : ILanguageOptionsProvider
    {
        private readonly ConcurrentDictionary<string, string> source;

        public LanguageOptionsProvider()
        {
            var cultureInfos = CultureInfo.GetCultures(CultureTypes.AllCultures);
            source = new ConcurrentDictionary<string, string>(cultureInfos.Select(x => new KeyValuePair<string, string>(x.TwoLetterISOLanguageName, x.Name)));
        }


        public IEnumerable<ILanguage> Languages()
        {
            return source.Select(x => new Language(x.Key, x.Value));
        }

        class Language : ValueObject<Language>, ILanguage
        {
            private readonly string key;
            private readonly string title;
            public Language(string key, string title)
            {
                Preconditions.NotEmpty(key, nameof(key));
                Preconditions.NotEmpty(title, nameof(title));
                this.key = key;
                this.title = title;
            }

            public string TwoLetterISO { get => key; set { throw new ApplicationException($"{nameof(TwoLetterISO)} is set at the constructor"); } }
            public string Title { get => title; set { throw new ApplicationException($"{nameof(Title)} is set at the constructor"); } }
        }
    }
}