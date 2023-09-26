using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Distributed;

namespace TrinsicV2WebWallet.Pages;

/// <summary>
/// Verify Credential universaty diploma
/// </summary>
public class CreateStudentDiplomaProofModel : PageModel
{
    private readonly GenerateProofService _walletService;
    private readonly IDistributedCache _distributedCache;

    [BindProperty(SupportsGet = true)]
    public string? Email { get; set; }

    [BindProperty]
    public string Code { get; set; } = string.Empty;

    [BindProperty]
    public List<SelectListItem>? Credentials { get; set; }

    [BindProperty]
    public string Credential { get; set; } = string.Empty;

    public string ProofDocumentJson { get; set; } = string.Empty;

    public CreateStudentDiplomaProofModel(GenerateProofService diplomaVerifyService,
        IDistributedCache distributedCache)
    {
        _walletService = diplomaVerifyService;
        _distributedCache = distributedCache;
    }

    public async Task OnGetAsync()
    {
        Credentials = await _walletService.GetItemsInWallet("");
    }

    public async Task OnPostAsync()
    {      
        var data = CacheData.GetFromCache(Email!, _distributedCache);
        if (data != null)
        {
            var authResult = _walletService.AuthenticateConfirm(Code, data.AuthenticateChallenge);

            var userCreateProof = await _walletService.CreateProof(
                authResult.AuthToken,
                Credential);

            ProofDocumentJson = userCreateProof.ProofDocumentJson;
        }
    }
}
