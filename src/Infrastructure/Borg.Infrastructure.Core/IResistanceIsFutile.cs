namespace Borg.Infrastructure.Core
{
    public interface IResistanceIsFutile
    {
        string Identifier { get; }
    }

    public class ResistanceIsFutile : IResistanceIsFutile
    {
        public virtual string Identifier => GetType().Name.SplitUpperCaseToWords();
    }
}