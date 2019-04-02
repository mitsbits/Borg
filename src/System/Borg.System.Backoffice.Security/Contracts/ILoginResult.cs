using Borg.Framework.DAL;

namespace Borg.System.Backoffice.Security.Contracts
{
    public interface ICmsUserOperationResult
    {
        bool Succeded { get; }
        ICmssUserError[] Errors { get; }
    }
    public interface ICmsUserLoginResult : ICmsUserOperationResult
    {

    }
    public interface ICmsUserSetPasswordResult : ICmsUserOperationResult
    {

    }

    public class CmsUserSetPasswordResult : CmsUserOperationResult, ICmsUserSetPasswordResult
    {
        public CmsUserSetPasswordResult(TransactionOutcome outcome, params CmssUserError[] usererrors) : base(outcome, usererrors)
        {

        }
    }

    public class CmsUserLoginResult : CmsUserOperationResult, ICmsUserLoginResult {
        public CmsUserLoginResult(TransactionOutcome outcome, params CmssUserError[] usererrors) :base(outcome, usererrors)
        {

        }
    }

    public abstract class CmsUserOperationResult : ICmsUserLoginResult
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