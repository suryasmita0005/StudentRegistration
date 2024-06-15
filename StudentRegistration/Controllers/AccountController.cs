using Microsoft.AspNet.Http.Features.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace StudentRegistration.Controllers
{
    [AllowAnonymous]
    [Route("MicrosoftIdentity/[controller]/[action]")]
    public class AccountController : Controller
    {
        [HttpGet("{scheme?}")]
        public IActionResult SignUpPolicy(
        [FromRoute] string scheme,
        [FromQuery] string redirectUri)
        {
            scheme ??= OpenIdConnectDefaults.AuthenticationScheme;
            string redirect;
            if (!string.IsNullOrEmpty(redirectUri) && Url.IsLocalUrl(redirectUri))
            {
                redirect = redirectUri;
            }
            else
            {
                redirect = Url.Content("~/")!;
            }

            scheme ??= OpenIdConnectDefaults.AuthenticationScheme;

            var properties = new AuthenticationProperties
            { RedirectUri = redirect };
            properties.Items[Constants.Policy] = "B2C_1_AdminSignUp";
            return Challenge(properties, scheme);
        }
    }
}
