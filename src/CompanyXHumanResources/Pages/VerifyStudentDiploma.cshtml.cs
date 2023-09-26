using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TrinsicV2WebWallet.Pages;

public class VerifyStudentDiplomaModel : PageModel
{
    private readonly DiplomaVerifyService _diplomaVerifyService;

    public bool UniversityCredentialIsValid { get; set; }

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
        var userCreateProof = ""; // UI init param

        var verifyUserCreatedProof = await _diplomaVerifyService.Verify(userCreateProof, nonce);

        var isValid = verifyUserCreatedProof.IsValid;

        UniversityCredentialIsValid = isValid;
    }
}
