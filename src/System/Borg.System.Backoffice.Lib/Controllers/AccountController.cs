using Borg.Framework;
using Borg.Platform.Backoffice.Security.EF;
using Borg.System.Backoffice.Core;
using Borg.System.Backoffice.Security;
using Borg.System.Backoffice.Security.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Borg.System.Backoffice.Lib.Controllers
{
    public class AccountController : BackOfficeController
    {
        private readonly ICmsUserManager<CmsUser> cmsUserManager;

        public AccountController(ICmsUserManager<CmsUser> cmsUserManager)
        {
            this.cmsUserManager = cmsUserManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await cmsUserManager.Login(model.Email, model.Password);
                //if (!result.Succeded)
                //{
                //    foreach (var error in result.Errors)
                //    {
                //        ModelState.AddModelError("", error.Exception.Message);
                //    }
                //    return View(model);
                //}

                var cmsUser = result.Payload;
                cmsUser = new CmsUser() { Email = "mitsbits@gmail.com", IsActive = true, Id = 1, Name = "mitsbits", Surname = "bitsanis" };

                var props = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                    ExpiresUtc = DateTime.UtcNow.AddYears(1)
                };
                var identity = new ClaimsIdentity(cmsUser.ToClaims(), BorgSecurityConstants.BackofficePolicyName);
                await HttpContext.SignInAsync(BorgSecurityConstants.BackofficePolicyName, new BorgClaimsPrincipal(new BorgIdentity()), props);

                return RedirectToLocal(returnUrl);
            }
            return View(model);
        }

        public class LoginViewModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }

            public string ReturnUrl { get; set; }
        }
    }
}