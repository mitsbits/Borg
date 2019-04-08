using Borg.Framework.DAL;

namespace Borg.System.Backoffice.Security.Contracts
{
    public class CmsUserSetPasswordResult : CmsUserOperationResult, ICmsUserSetPasswordResult
    {
        public CmsUserSetPasswordResult(TransactionOutcome outcome, params CmssUserError[] usererrors) : base(outcome, usererrors)
        {
        }
    }
    public class CmsUserLoginResult : CmsUserOperationResult, ICmsUserLoginResult
    {
        public CmsUserLoginResult(TransactionOutcome outcome, User payload, params CmssUserError[] usererrors) : base(outcome, usererrors)
        {
            Payload = payload;
        }

        public User Payload { get; private set; }
    }

    public abstract class CmsUserOperationResult : ICmsUserOperationResult
    {
        private readonly TransactionOutcome _outcome;
        private readonly CmssUserError[] _usererrors;

        protected CmsUserOperationResult(TransactionOutcome outcome, params CmssUserError[] usererrors)
        {
            _outcome = outcome;
            _usererrors = usererrors;
        }

        public bool Succeded => _outcome == TransactionOutcome.Success;

        public ICmssUserError[] Errors => _usererrors;
    }
}