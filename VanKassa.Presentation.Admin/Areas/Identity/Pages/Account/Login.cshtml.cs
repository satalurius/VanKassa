using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VanKassa.Backend.Core.Data;
using VanKassa.Backend.Core.Services;

namespace VanKassa.Presentation.Admin.Areas.Identity.Pages.Account;

public class Login : PageModel
{
    private readonly CookieIdentity _cookieIdentity;
    
    public Login(CookieIdentity cookieIdentity)
    {
        _cookieIdentity = cookieIdentity;
    }

    [BindProperty] public Domain.Models.Login LoginModel { get; set; } = new();
    

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var loginUserData = await _cookieIdentity.CanLogin(LoginModel);

        if (!loginUserData.canLogin)
        {
            return Page();
        }

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, loginUserData.usersClaims);
        return LocalRedirect(Routes.EmployeesTablePage);

    }
}