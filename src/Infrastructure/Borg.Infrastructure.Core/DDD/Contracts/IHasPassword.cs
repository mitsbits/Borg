namespace Borg.Infrastructure.Core.DDD.Contracts
{
    public interface IHasPassword
    {
        string PasswordHash { get; set; }
    }
}