using Microsoft.AspNetCore.Mvc.RazorPages;
using Trinsic;
using Trinsic.Services.UniversalWallet.V1;

namespace University.Pages;

/// <summary>
/// Create Credential Template for a new universaty diploma
/// https://docs.trinsic.id/reference/services/template-service/
/// </summary>
public class CreateUniversityIssuerModel : PageModel
{
    private readonly TrinsicService _trinsicService;
    private readonly IConfiguration _configuration;
    private readonly UniversityServices _universityServices;

    public string WalletId { get; set; } = string.Empty;
    public string AuthToken { get; set; } = string.Empty;

    public CreateUniversityIssuerModel(TrinsicService trinsicService,
        UniversityServices universityServices,
        IConfiguration configuration)
    {
        _universityServices = universityServices;
        _trinsicService = trinsicService;
        _configuration = configuration;
    }

    public void OnGet()
    {
    }

    public async Task OnPostAsync()
    {
        var createWalletResponse = await _universityServices.CreateUniversityWalletToIssueDiplomas();

        // This authToken and walletId is required to issue credentials
        AuthToken = createWalletResponse!.AuthToken;
        WalletId = createWalletResponse!.Wallet.WalletId;

        // Get template to validate that it exists
        var credentialTemplate = await _universityServices.GetUniversityDiplomaTemplate(
            _universityServices.GetUniversityDiplomaTemplateId());

        await _universityServices.RegisterIssuer(WalletId,
            credentialTemplate.Template.SchemaUri);
    }   
}
