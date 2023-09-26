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

        var authInit = _diplomaVerifyService.AuthenticateInit("damien_bod@hotmail.com");

        // add code from email
        var code = "";
        var authResult = _diplomaVerifyService.AuthenticateConfirm(code, authInit);

        var userCreateProof = await _diplomaVerifyService.CreateProof(proofTemplateId, nonce, authResult.AuthToken);

        var verifyUserCreatedProof = await _diplomaVerifyService.Verify(userCreateProof, nonce);

        var isValid = verifyUserCreatedProof.IsValid;

        ProofDocumentJson = userCreateProof!.ProofDocumentJson;
    }
}
