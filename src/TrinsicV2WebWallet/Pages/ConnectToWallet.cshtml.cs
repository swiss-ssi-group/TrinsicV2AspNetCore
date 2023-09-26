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

    public void Get()
    {
       
    }

    public IActionResult OnPost()
    {
        var authInit = _walletService.AuthenticateInit("damien_bod@hotmail.com");

        var cacheData = new CacheData
        {
            AuthenticateChallenge = authInit.Challenge
        };

        CacheData.AddToCache("damien_bod@hotmail.com", _distributedCache, cacheData);

        return Redirect($"~/CreateStudentDiplomaProof");
    }
}
