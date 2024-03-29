using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompanyXHumanResources.Pages;

public class VerifyStudentDiplomaModel : PageModel
{
    private readonly DiplomaVerifyService _diplomaVerifyService;
    public bool UniversityCredentialIsValid { get; set; }

    public VerifyStudentDiplomaModel(DiplomaVerifyService diplomaVerifyService)
    {
        _diplomaVerifyService = diplomaVerifyService;
    }

    [BindProperty]
    public string DiplomaProof {  get; set; } = string.Empty;

    [BindProperty]
    public List<SelectListItem>? Universities { get; set; }

    [BindProperty]
    public string University { get; set; } = string.Empty;

    public void OnGet()
    {
        Universities = _diplomaVerifyService.TrustedUniversities;
    }

    public async Task OnPostAsync()
    {
        var (_, IsValid) = await _diplomaVerifyService.Verify(DiplomaProof, University);
        UniversityCredentialIsValid = IsValid;
    }
}
