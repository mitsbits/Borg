namespace Borg.Infrastructure.Core
{
    public interface IResistanceIsFutile
    {
        string Identifier { get; }
    }

    public class ResistanceIsFutile : IResistanceIsFutile
    {
        public string Identifier => "Resistance Is Futile";
    }
}