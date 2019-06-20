using Borg.Framework.Cms;
using Borg.Framework.Cms.Contracts;
using Borg.Framework.DAL;
using Borg.Framework.EF.Contracts;
using Borg.Framework.EF.DAL;
using Borg.Infrastructure.Core.Services.Security;
using Borg.System.Backoffice.Security.Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Threading.Tasks;

namespace Borg.Platform.EF.CMS.Security
{
    public class CmsUserManager : UnitOfWork<BorgDb>, IUnitOfWork<BorgDb>, ICmsUserManager<CmsUser>
    {
        private readonly ILogger logger;
        private readonly ICmsUserPasswordValidator passwordValidator;
        private readonly ICrypto crypto;

        public CmsUserManager(ILoggerFactory loggerfactory, BorgDb db, ICmsUserPasswordValidator passwordValidator, ICrypto crypto) : base(loggerfactory, db)
        {
            this.logger = loggerfactory == null ? NullLogger.Instance : loggerfactory.CreateLogger(GetType());
            this.passwordValidator = passwordValidator;
            this.crypto = crypto;
        }

        public async Task<ICmsOperationResult<CmsUser>> Login(string user, string password)
        {
            var cmsuser = await QueryRepo<CmsUser>().Get(x => x.Email.ToLowerInvariant() == user.ToLowerInvariant());
            if (cmsuser == null)
            {
                logger.Debug($"no user for {nameof(user)}:{user}");
                return new CmsOperationResult<CmsUser>(TransactionOutcome.Failure, null, new CmsError(user, new ApplicationException($"no user for {nameof(user)}:{user}")));
            }
            if (!cmsuser.IsActive)
            {
                logger.Debug($"not active user for {nameof(user)}:{user}");
                return new CmsOperationResult<CmsUser>(TransactionOutcome.Failure, null, new CmsError(user, new ApplicationException($"not active user for {nameof(user)}:{user}")));
            }
            var passwordmatch = crypto.VerifyHashedPassword(cmsuser.PasswordHash, password);
            if (!passwordmatch)
            {
                logger.Debug($"invalid password for {nameof(user)}:{user}");
                return new CmsOperationResult<CmsUser>(TransactionOutcome.Failure, null, new CmsError(user, new ApplicationException($"invalid password for {nameof(user)}:{user}")));
            }
            logger.Debug($"succesful login for {nameof(user)}:{user}");
            return new CmsOperationResult<CmsUser>(TransactionOutcome.Success, cmsuser);
        }

        public async Task<ICmsOperationResult> SetPassword(string user, string password)
        {
            var cmsuser = await ReadWriteRepo<CmsUser>().Get(x => x.Email.ToLowerInvariant() == user.ToLowerInvariant());
            if (cmsuser == null)
            {
                logger.Debug($"no user for {nameof(user)}:{user}");
                return new CmsOperationResult(TransactionOutcome.Failure, new CmsError(user));
            }
            var validatorresult = await passwordValidator.IsStrongEnough(password);
            {
                if (!validatorresult.isStrong)
                {
                    logger.Debug($"not strong enough password for {nameof(user)}:{user}");
                    return new CmsOperationResult(TransactionOutcome.Failure, new CmsError(user));
                }
            }
            try
            {
                cmsuser.PasswordHash = crypto.HashPassword(password);
                cmsuser = await ReadWriteRepo<CmsUser>().Update(cmsuser);
                await Save();
                return new CmsOperationResult(TransactionOutcome.Success);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"failed setting password for {nameof(user)}:{user}");
                return new CmsOperationResult(TransactionOutcome.Failure, new CmsError(user, ex));
            }
        }
    }
}