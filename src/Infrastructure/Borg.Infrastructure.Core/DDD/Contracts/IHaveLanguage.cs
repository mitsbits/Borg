namespace Borg.Infrastructure.Core.DDD.Contracts
{
    public interface IHaveLanguage<out TLanguage> where TLanguage : IGlobalizationSilo
    {
        string TwoLetterISO { get; }
        TLanguage Language { get; }
    }
}