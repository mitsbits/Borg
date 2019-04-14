using Borg.Framework.EF;
using Microsoft.EntityFrameworkCore;

namespace Borg.Platform.Backoffice.Security.EF.Data
{
    public class SecurityDbContext : BorgDbContext
    {
        public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options)
        {
        }
    }
}