using Borg.Framework.DAL;

namespace Borg.System.Backoffice.Security.Contracts
{
    public class CmsUserSetPasswordResult : CmsUserOperationResult, ICmsUserSetPasswordResult
    {
        public CmsUserSetPasswordResult(TransactionOutcome outcome, params CmssUserError[] usererrors) : base(outcome, usererrors)
        {
        }
    }

    public class CmsUserLoginResult<TData> : CmsUserOperationResult, ICmsUserLoginResult<TData>
    {
        public CmsUserLoginResult(TransactionOutcome outcome, TData payload, params CmssUserError[] usererrors) : base(outcome, usererrors)
        {
            Payload = payload;
        }

        public TData Payload { get; private set; }
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