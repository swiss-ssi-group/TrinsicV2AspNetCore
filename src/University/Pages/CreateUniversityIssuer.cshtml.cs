using Microsoft.AspNetCore.Mvc.RazorPages;
using Trinsic;
using Trinsic.Services.TrustRegistry.V1;
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

    public CreateUniversityIssuerModel(TrinsicService trinsicService, IConfiguration configuration)
    {
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
            EcosystemId = _configuration["TrinsicOptions:ApiKey"],
            Description = "wallet to issue university diplomas"
        };

        var createWalletResponse = await _trinsicService.Wallet.CreateWalletAsync(request);

        var authToken = createWalletResponse.AuthToken;

        await RegisterIssuer(createWalletResponse.Wallet.WalletId,
            createWalletResponse.Wallet.Name);
    }

    private async Task<RegisterMemberResponse> RegisterIssuer(string walletId, string schemaUri)
    {
        // original API 
        _trinsicService.Options.AuthToken = _configuration["TrinsicOptions:ApiKey"];

        return await _trinsicService.TrustRegistry.RegisterMemberAsync(new()
        {
            WalletId = walletId,
            SchemaUri = schemaUri,
        });
    }
}
