using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrinsicV2WebWallet.Pages;

/// <summary>
/// Verify Credential universaty diploma
/// </summary>
[Authorize]
public class CreateStudentDiplomaProofModel : PageModel
{
    private readonly GenerateProofService _walletService;

    [BindProperty]
    public List<SelectListItem>? Credentials { get; set; }

    [BindProperty]
    public string Credential { get; set; } = string.Empty;

    public string ProofDocumentJson { get; set; } = string.Empty;

    public CreateStudentDiplomaProofModel(GenerateProofService diplomaVerifyService)
    {
        _walletService = diplomaVerifyService;
    }

    public async Task OnGetAsync()
    {
        var walletAuthClaim = User.Claims.FirstOrDefault(c => c.Type == "authResult");
        Credentials = await _walletService.GetItemsInWallet(walletAuthClaim!.Value);
    }

    public async Task OnPostAsync()
    {
        var walletAuthClaim = User.Claims.FirstOrDefault(c => c.Type == "authResult");
 
        var userCreateProof = await _walletService.CreateProof(
            walletAuthClaim!.Value,
            Credential);

        ProofDocumentJson = userCreateProof.ProofDocumentJson;
    }
}
