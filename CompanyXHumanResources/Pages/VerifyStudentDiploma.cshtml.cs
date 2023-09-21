using CompanyXHumanResources;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CompanyXHumanResources.Pages;

/// <summary>
/// Verify Credential universaty diploma

/// </summary>
public class VerifyStudentDiplomaModel : PageModel
{
    private readonly DiplomaVerifyService _diplomaVerifyService;

    public string ProofDocumentJson { get; set; } = string.Empty;

    public VerifyStudentDiplomaModel(DiplomaVerifyService diplomaVerifyService)
    {
        _diplomaVerifyService = diplomaVerifyService;
    }

    public void Get()
    {
    }

    public async Task OnPostAsync()
    {

        var response = await _diplomaVerifyService.Verify(
            "urn:template:peaceful-booth-zrpufxfp6l3c:verifydiplomapresentation");

        ProofDocumentJson = response!.ProofDocumentJson;
    }
}
