using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.AspNetCore.Authentication;

namespace TrinsicV2WebWallet.Pages;

/// <summary>
/// Verify Credential universaty diploma
/// </summary>
public class ConnectedToWalletModel : PageModel
{
    private readonly GenerateProofService _walletService;
    private readonly IDistributedCache _distributedCache;

    [BindProperty(SupportsGet = true)]
    public string? Email { get; set; }

    [BindProperty]
    public string Code { get; set; } = string.Empty;

    public string ProofDocumentJson { get; set; } = string.Empty;

    public ConnectedToWalletModel(GenerateProofService diplomaVerifyService,
        IDistributedCache distributedCache)
    {
        _walletService = diplomaVerifyService;
        _distributedCache = distributedCache;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {      
        var data = CacheData.GetFromCache(Email!, _distributedCache);
        if (data != null)
        {
            var authResult = _walletService.AuthenticateConfirm(Code, data.AuthenticateChallenge);

            // setup session
            var claims = new List<Claim>()
            {
                new Claim("authResult",authResult.AuthToken),
                new Claim("email",Email!)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme,
                "name","role");

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return Redirect($"~/CreateStudentDiplomaProof");
        }

        return Redirect($"~/Error");
    }
}
