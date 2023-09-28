using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Trinsic;
using Trinsic.Services.Provider.V1;
using Trinsic.Services.UniversalWallet.V1;
using Trinsic.Services.VerifiableCredentials.V1;

namespace TrinsicV2WebWallet;

public class GenerateProofService
{
    private readonly TrinsicService _trinsicService;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// This would be some public list of university diploma schemes
    /// Don't think this will work because not every uni will used the same SSI, id-tech systems, standards
    /// </summary>
    public readonly List<SelectListItem> Universities = new List<SelectListItem>
    {
        new SelectListItem { Text ="University SSI Schweiz", Value= "peaceful-booth-zrpufxfp6l3c"}
    };

    public GenerateProofService(TrinsicService trinsicService, IConfiguration configuration)
    {
        _trinsicService = trinsicService;
        _configuration = configuration;
    }

    public async Task<List<SelectListItem>> GetItemsInWallet(string userAuthToken)
    {
        var results = new List<SelectListItem>();
        // Auth token from user 
        _trinsicService.Options.AuthToken = userAuthToken;

        // get all items
        var items = await _trinsicService.Wallet.SearchWalletAsync(new SearchRequest());
        foreach (var item in items.Items)
        {
            var jsonObject = JsonNode.Parse(item)!;
            var id = jsonObject["id"];
            var vcArray = jsonObject["data"]!["type"];
            var vc = string.Empty;
            foreach (var i in vcArray!.AsArray())
            {
                var val = i!.ToString();
                if (val != "VerifiableCredential")
                {
                    vc = val!.ToString();
                    break;
                }
            }

            results.Add(new SelectListItem(vc, id!.ToString()));
        }

        return results;
    }

    public async Task<CreateProofResponse> CreateProof(string userAuthToken, string credentialItemId)
    {
        // Auth token from user 
        _trinsicService.Options.AuthToken = userAuthToken;

        //var createProofResponse = await _trinsicService.Credential.CreateProofAsync(new CreateProofRequest
        //{
        //    ItemId = credentialItemId,
        //});

        var selectiveProof = await _trinsicService.Credential.CreateProofAsync(new()
        {
            ItemId = credentialItemId,
            RevealTemplate = new()
            {
                TemplateAttributes = { "firstName", "lastName", "dateOfBirth", "diplomaTitle" }
            }
        });

        return selectiveProof;
    }

    public AuthenticateInitResponse AuthenticateInit(string userId, string universityEcosystemId)
    {
        var requestInit = new AuthenticateInitRequest
        {
            Identity = userId,
            Provider = IdentityProvider.Email,
            EcosystemId = universityEcosystemId
        };

        var authenticateInitResponse = _trinsicService.Wallet.AuthenticateInit(requestInit);

        return authenticateInitResponse;
    }

    public AuthenticateConfirmResponse AuthenticateConfirm(string code, string challenge)
    {
        var requestConfirm = new AuthenticateConfirmRequest
        {
            Challenge = challenge,
            Response = code
        };
        var authenticateConfirmResponse = _trinsicService.Wallet.AuthenticateConfirm(requestConfirm);

        return authenticateConfirmResponse;
    }
}
