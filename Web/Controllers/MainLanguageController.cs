using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Web.Controllers
{
    public class MainLanguageController : Controller
    {
        public IActionResult Language(string? langKey, string? title)
        {

            if(langKey != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(langKey);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(langKey);
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("az");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("az");
            }

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(langKey)),
                new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );

            CookieOptions cookieOptions = new();
            cookieOptions.Expires = DateTime.Now.AddDays(1);
          Response.Cookies.Append("Duckalang", langKey, cookieOptions);
            return RedirectToAction("Index", "Home");
        }
    }
}
