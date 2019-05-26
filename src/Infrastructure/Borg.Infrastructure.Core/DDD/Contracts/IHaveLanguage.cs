namespace Borg.Infrastructure.Core.DDD.Contracts
{
    public interface IHaveLanguage<out TLanguage> where TLanguage : ILanguage
    {
        string TwoLetterISO { get; set; }
        TLanguage Language { get; }
    }
}