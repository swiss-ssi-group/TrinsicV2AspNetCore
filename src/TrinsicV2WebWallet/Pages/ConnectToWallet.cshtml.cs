using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    public string Email { get; set; } = string.Empty;

    public void OnGet()
    {
       
    }

    public IActionResult OnPost()
    {
        var authInit = _walletService.AuthenticateInit(Email);

        var cacheData = new CacheData
        {
            AuthenticateChallenge = authInit.Challenge
        };

        CacheData.AddToCache(Email, _distributedCache, cacheData);

        return Redirect($"~/CreateStudentDiplomaProof/{Email}");
    }
}
