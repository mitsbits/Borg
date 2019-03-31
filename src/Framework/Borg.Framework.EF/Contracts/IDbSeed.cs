using System.Threading.Tasks;

namespace Borg.Framework.EF.Contracts
{
    public interface IDbSeed
    {
        Task EnsureUp();
    }
}