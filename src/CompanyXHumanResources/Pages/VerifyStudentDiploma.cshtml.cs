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
        var nonce = "fdfdgfhrgtbh67jjvf3fc3_fr4-(4f";
        var proofTemplateId = "urn:template:peaceful-booth-zrpufxfp6l3c:verifydiplomapresentation";

        var userCreateProof = await _diplomaVerifyService.CreateProof(proofTemplateId, nonce);

        var verifyUserCreatedProof = await _diplomaVerifyService.Verfiy(userCreateProof, nonce);

        var isValid = verifyUserCreatedProof.IsValid;

        ProofDocumentJson = userCreateProof!.ProofDocumentJson;
    }
}
