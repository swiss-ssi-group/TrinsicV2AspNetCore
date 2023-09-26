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

    public string ProofDocumentJson { get; set; } = string.Empty;

    public CreateStudentDiplomaProofModel(GenerateProofService diplomaVerifyService,
        IDistributedCache distributedCache)
    {
        _walletService = diplomaVerifyService;
        _distributedCache = distributedCache;
    }

    public void Get()
    {
       
    }

    public async Task OnPostAsync()
    {
        var code = "";
        var email = "damien_bod@hotmail.com";
        var data = CacheData.GetFromCache(email, _distributedCache);
        if (data != null)
        {
            Debug.WriteLine("check if there was a response yet: " + data);

            var authResult = _walletService.AuthenticateConfirm(code, data.AuthenticateChallenge);

            var credentialItemId = "urn:uuid:777c3ce9-22b8-4f70-98ce-c8870f5f4c0d";
            var userCreateProof = await _walletService.CreateProof(
                authResult.AuthToken,
                credentialItemId);


            ProofDocumentJson = userCreateProof.ProofDocumentJson;
        }
    }
}
