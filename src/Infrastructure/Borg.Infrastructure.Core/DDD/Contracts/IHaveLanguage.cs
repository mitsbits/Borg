namespace Borg.Infrastructure.Core.DDD.Contracts
{
    public interface IHaveLanguage
    {
        string TwoLetterISO { get; set; }
        ILanguage Language { get; }
    }
}