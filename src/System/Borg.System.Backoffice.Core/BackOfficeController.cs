using Borg.Framework.Modularity;
using Borg.System.Backoffice.Core.Security.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Borg.System.Backoffice.Core
{
    [Area("Backoffice")]
    [BorgAuthorize]
    public abstract class BackOfficeController : Controller
    {
        protected readonly ILogger logger;
        protected readonly IUserSession userSession;

        protected BackOfficeController(ILoggerFactory loggerFactory, IUserSession userSession)
        {
            this.logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger(GetType());
            this.userSession = userSession;
        }

        [NonAction]
        protected IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return Redirect(Url.Content("~/"));
        }

        #region Pager

        private const string pageNumerVariableName = "p";
        private const string rowCountVariableName = "r";

        private RequestPager _pager;

        protected RequestPager Pager()
        {
            if (_pager != null) return _pager;
            var p = 1;
            var r = 10;
            if (this.Request.Query.ContainsKey(pageNumerVariableName)) p = int.Parse(this.Request.Query[pageNumerVariableName].ToString());
            if (this.Request.Query.ContainsKey(rowCountVariableName)) r = int.Parse(this.Request.Query[rowCountVariableName].ToString());

            _pager = new RequestPager() { Current = p, RowCount = r };
            return _pager;
        }

        protected class RequestPager
        {
            public int Current { get; internal set; }
            public int RowCount { get; internal set; }
        }

        #endregion Pager
    }
}