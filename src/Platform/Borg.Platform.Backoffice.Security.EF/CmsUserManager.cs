using Borg.Framework.DAL;
using Borg.Framework.EF.Contracts;
using Borg.Framework.EF.DAL;
using Borg.Platform.Backoffice.Security.EF.Data;
using Borg.System.Backoffice.Security;
using Borg.System.Backoffice.Security.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Borg.Platform.Backoffice.Security.EF
{
    public class CmsUserManager : UnitOfWork<SecurityDbContext>, IUnitOfWork<SecurityDbContext>, ICmsUserManager<CmsUser>
    {
        private readonly ILogger _logger;
        private readonly ICmsUserPasswordValidator _passwordValidator;

        public CmsUserManager(ILoggerFactory loggerFactory, SecurityDbContext db, ICmsUserPasswordValidator passwordValidator) : base(db)
        {
            _logger = loggerFactory.CreateLogger<CmsUserManager>();
            _passwordValidator = passwordValidator;
        }

        public async Task<ICmsUserLoginResult<CmsUser>> Login(string user, string password)
        {
            var cmsuser = await QueryRepo<CmsUser>().Get(x => x.Email.ToLowerInvariant() == user.ToLowerInvariant());
            if (cmsuser == null)
            {
                _logger.Debug($"no user for {nameof(user)}:{user}");
                return new CmsUserLoginResult<CmsUser>(TransactionOutcome.Failure, null, new CmssUserError(user));
            }
            if (!cmsuser.IsActive)
            {
                _logger.Debug($"not active user for {nameof(user)}:{user}");
                return new CmsUserLoginResult<CmsUser>(TransactionOutcome.Failure, null, new CmssUserError(user));
            }
            var passwordmatch = Crypto.VerifyHashedPassword(cmsuser.PasswordHash, password);
            if (!passwordmatch)
            {
                _logger.Debug($"invalid password for {nameof(user)}:{user}");
                return new CmsUserLoginResult<CmsUser>(TransactionOutcome.Failure, null, new CmssUserError(user));
            }
            _logger.Debug($"succesful login for {nameof(user)}:{user}");
            return new CmsUserLoginResult<CmsUser>(TransactionOutcome.Success, cmsuser);
        }

        public async Task<ICmsUserSetPasswordResult> SetPassword(string user, string password)
        {
            var cmsuser = await ReadWriteRepo<CmsUser>().Get(x => x.Email.ToLowerInvariant() == user.ToLowerInvariant());
            if (cmsuser == null)
            {
                _logger.Debug($"no user for {nameof(user)}:{user}");
                return new CmsUserSetPasswordResult(TransactionOutcome.Failure, new CmssUserError(user));
            }
            var validatorresult = await _passwordValidator.IsStrongEnough(password);
            {
                if (!validatorresult.isStrong)
                {
                    _logger.Debug($"not strong enough password for {nameof(user)}:{user}");
                    return new CmsUserSetPasswordResult(TransactionOutcome.Failure, new CmssUserError(user));
                }
            }
            try
            {
                cmsuser.PasswordHash = Crypto.HashPassword(password);
                cmsuser = await ReadWriteRepo<CmsUser>().Update(cmsuser);
                await Save();
                return new CmsUserSetPasswordResult(TransactionOutcome.Success);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"failed setting password for {nameof(user)}:{user}");
                return new CmsUserSetPasswordResult(TransactionOutcome.Failure, new CmssUserError(user, ex));
            }
        }
    }
}