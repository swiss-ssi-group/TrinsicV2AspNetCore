using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TrinsicV2WebWallet.Pages;

public class VerifyStudentDiplomaModel : PageModel
{
    private readonly DiplomaVerifyService _diplomaVerifyService;

    public bool UniversityCredentialIsValid { get; set; }

    [BindProperty]
    public string DiplomaProof {  get; set; } = string.Empty;

    public VerifyStudentDiplomaModel(DiplomaVerifyService diplomaVerifyService)
    {
        _diplomaVerifyService = diplomaVerifyService;
    }

    public void Get()
    {
    }

    public async Task OnPostAsync()
    {
        var verify = await _diplomaVerifyService.Verify(DiplomaProof);

        UniversityCredentialIsValid = verify.IsValid;
    }
}
