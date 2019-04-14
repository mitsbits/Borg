using System.Threading.Tasks;

namespace Borg.Framework.EF.Contracts
{
    public interface IDbRecipe
    {
        Task Populate();
    }
}