using Borg.System.Licencing.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Borg.System.AddOn
{
    public class LicenceMiddleware
    {
        private IBorgLicenceService _borgLicenceService;
        private readonly IBorgLicence _borgLicence;
        private readonly RequestDelegate _next;

        public LicenceMiddleware(RequestDelegate next, IBorgLicenceService borgLicenceService)
        {
            _borgLicenceService = borgLicenceService;
            if (_borgLicence == null) _borgLicence = _borgLicenceService.Retrieve();
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (_borgLicence.ActiveApplicationServerCount() == 0) throw new InvalidOperationException("no licence");
            var cultureQuery = context.Request.Query["culture"];

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}