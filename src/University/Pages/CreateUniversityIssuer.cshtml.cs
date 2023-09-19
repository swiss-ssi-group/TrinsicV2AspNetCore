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
        var request = new CreateWalletRequest
        {
            EcosystemId = _configuration["TrinsicOptions:Ecosystem"],
            Description = "wallet to issue university diplomas"
        };

        var createWalletResponse = await _universityServices
            .CreateUniversityWalletToIssueDiplomas(request);

        // This authToken and walletId is required to issue credentials
        // Add this to the DB, user secrets or key vault
        AuthToken = createWalletResponse!.AuthToken;
        WalletId = createWalletResponse!.Wallet.WalletId;
    }   
}
