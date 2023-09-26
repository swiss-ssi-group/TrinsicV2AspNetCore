using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TrinsicV2WebWallet.Pages;

/// <summary>
/// Verify Credential universaty diploma

/// </summary>
public class CreateStudentDiplomaProofModel : PageModel
{
    private readonly GenerateProofService _walletService;

    public string ProofDocumentJson { get; set; } = string.Empty;

    public CreateStudentDiplomaProofModel(GenerateProofService diplomaVerifyService)
    {
        _walletService = diplomaVerifyService;
    }

    public void Get()
    {
    }

    public async Task OnPostAsync()
    {
        var nonce = "fdfdgfhrgtbh67jjvf3fc3_fr4-(4f";
        var proofTemplateId = "urn:template:peaceful-booth-zrpufxfp6l3c:verifydiplomapresentation";

        var authInit = _walletService.AuthenticateInit("damien_bod@hotmail.com");

        // add code from email
        var code = "";
        var authResult = _walletService.AuthenticateConfirm(code, authInit);

        var credentialItemId = "urn:uuid:777c3ce9-22b8-4f70-98ce-c8870f5f4c0d";
        var userCreateProof = await _walletService.CreateProof(
            proofTemplateId,
            nonce,
            authResult.AuthToken,
            credentialItemId);


        ProofDocumentJson = userCreateProof.ProofDocumentJson;
    }
}
