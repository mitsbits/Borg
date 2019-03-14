namespace Borg.System.DDD.Contracts
{
    public interface IHasPassword
    {
        string PasswordHash { get; set; }
    }
}