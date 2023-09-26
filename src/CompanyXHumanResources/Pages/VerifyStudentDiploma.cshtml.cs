using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrinsicV2WebWallet.Pages;

public class VerifyStudentDiplomaModel : PageModel
{
    private readonly DiplomaVerifyService _diplomaVerifyService;

    public bool UniversityCredentialIsValid { get; set; }

    [BindProperty]
    public string DiplomaProof {  get; set; } = string.Empty;

    [BindProperty]
    public List<SelectListItem>? Universities { get; set; }

    [BindProperty]
    public string University { get; set; } = string.Empty;

    public VerifyStudentDiplomaModel(DiplomaVerifyService diplomaVerifyService)
    {
        _diplomaVerifyService = diplomaVerifyService;
    }

    public void Get()
    {
        Universities = _diplomaVerifyService.TrustedUniversities;
    }

    public async Task OnPostAsync()
    {
        var verify = await _diplomaVerifyService.Verify(DiplomaProof, University);

        UniversityCredentialIsValid = verify.IsValid;
    }
}
