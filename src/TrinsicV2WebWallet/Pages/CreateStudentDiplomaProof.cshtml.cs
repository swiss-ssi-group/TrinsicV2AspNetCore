using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

    public string ProofDocumentJson { get; set; } = string.Empty;

    public CreateStudentDiplomaProofModel(GenerateProofService diplomaVerifyService,
        IDistributedCache distributedCache)
    {
        _walletService = diplomaVerifyService;
        _distributedCache = distributedCache;
    }

   
    public void OnGet()
    {  
    }

    public async Task OnPostAsync()
    {
        
        var data = CacheData.GetFromCache(Email!, _distributedCache);
        if (data != null)
        {
            var authResult = _walletService.AuthenticateConfirm(Code, data.AuthenticateChallenge);

            var credentialItemId = "urn:uuid:777c3ce9-22b8-4f70-98ce-c8870f5f4c0d";
            var userCreateProof = await _walletService.CreateProof(
                authResult.AuthToken,
                credentialItemId);


            ProofDocumentJson = userCreateProof.ProofDocumentJson;
        }
    }
}
