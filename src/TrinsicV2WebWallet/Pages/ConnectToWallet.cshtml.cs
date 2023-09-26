using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Distributed;

namespace TrinsicV2WebWallet.Pages;

public class ConnectToWalletModel : PageModel
{
    private readonly GenerateProofService _walletService;
    private readonly IDistributedCache _distributedCache;

    public string ProofDocumentJson { get; set; } = string.Empty;

    public ConnectToWalletModel(GenerateProofService diplomaVerifyService,
        IDistributedCache distributedCache)
    {
        _walletService = diplomaVerifyService;
        _distributedCache = distributedCache;
    }

    [BindProperty]
    public List<SelectListItem>? Universities { get; set; }

    [BindProperty]
    public string University { get; set; } = string.Empty;

    [BindProperty]
    public string Email { get; set; } = string.Empty;

    public void OnGet()
    {
        Universities = _walletService.Universities;
    }

    public IActionResult OnPost()
    {
        var authInit = _walletService.AuthenticateInit(Email, University);

        var cacheData = new CacheData
        {
            AuthenticateChallenge = authInit.Challenge
        };

        CacheData.AddToCache(Email, _distributedCache, cacheData);

        return Redirect($"~/ConnectedToWalletCode/{Email}");
    }
}
