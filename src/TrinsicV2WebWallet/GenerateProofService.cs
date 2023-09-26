using Google.Protobuf;
using System.Text;
using Trinsic;
using Trinsic.Services.Provider.V1;
using Trinsic.Services.UniversalWallet.V1;
using Trinsic.Services.VerifiableCredentials.V1;

namespace TrinsicV2WebWallet;

public class GenerateProofService
{
    private readonly TrinsicService _trinsicService;
    private readonly IConfiguration _configuration;

    public GenerateProofService(TrinsicService trinsicService, IConfiguration configuration)
    {
        _trinsicService = trinsicService;
        _configuration = configuration;
    }

    public async Task<CreateProofResponse>  CreateProof(string universityTemplateId,
        string nonce, string userAuthToken, string credentialItemId)
    {
        // Auth token from user 
        _trinsicService.Options.AuthToken = userAuthToken;

        // get all items
        var items = await _trinsicService.Wallet.SearchWalletAsync(new SearchRequest());

        var createProofResponse = await _trinsicService.Credential.CreateProofAsync(new CreateProofRequest
        {
            VerificationTemplateId = universityTemplateId,
            ItemId = credentialItemId,
            Nonce = ByteString.CopyFrom(nonce, Encoding.Unicode)
        });

        //var selectiveProof = await _trinsicService.Credential.CreateProofAsync(new()
        //{
        //    DocumentJson = credentialJson.DocumentJson,
        //    RevealTemplate = new()
        //    {
        //        // The other field, not disclosed, is "age"
        //        TemplateAttributes = { "firstName", "lastName" }
        //    }
        //});

        return createProofResponse;
    }

    public AuthenticateInitResponse AuthenticateInit(string userId)
    {
        var universityEcoSystemId = _configuration["TrinsicUniversityOptions:Ecosystem"];

        var requestInit = new AuthenticateInitRequest
        {
            Identity = userId,
            Provider = IdentityProvider.Email,
            EcosystemId = universityEcoSystemId
        };

        var authenticateInitResponse = _trinsicService.Wallet.AuthenticateInit(requestInit);

        return authenticateInitResponse;
    }

    public AuthenticateConfirmResponse AuthenticateConfirm(string code, AuthenticateInitResponse authenticateInitResponse)
    {
        var requestConfirm = new AuthenticateConfirmRequest
        {
            Challenge = authenticateInitResponse.Challenge,
            Response = code
        };
        var authenticateConfirmResponse = _trinsicService.Wallet.AuthenticateConfirm(requestConfirm);

        return authenticateConfirmResponse;
    }
}
