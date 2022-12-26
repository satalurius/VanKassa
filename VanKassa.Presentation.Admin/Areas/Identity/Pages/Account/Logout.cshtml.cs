using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VanKassa.Backend.Core.Data;

namespace VanKassa.Presentation.Admin.Areas.Identity.Pages.Account;

public class Logout : PageModel
{
    public async Task<IActionResult> OnGetAsync()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return LocalRedirect(Routes.EmployeesTablePage);
    }
}