using Google.Protobuf;
using System.Text;
using Trinsic;
using Trinsic.Services.Provider.V1;
using Trinsic.Services.UniversalWallet.V1;
using Trinsic.Services.VerifiableCredentials.V1;

namespace CompanyXHumanResources;

public class DiplomaVerifyService
{
    private readonly TrinsicService _trinsicService;
    private readonly IConfiguration _configuration;
  
    /// <summary>
    /// This would be some public list of university diploma schemes
    /// Don't think this will work because not every uni will used the same SSI, id-tech systems, standards
    /// </summary>
    private readonly List<string> _trustedUniversitySchemes = new List<string>
    {
        "https://schema.trinsic.cloud/peaceful-booth-zrpufxfp6l3c/diploma-credential-for-swiss-self-sovereign-identity-ssi"
    };

    /// <summary>
    /// Get a list of trusted Universities
    /// Send verification presentation
    /// Validate VC
    /// </summary>
    public DiplomaVerifyService(TrinsicService trinsicService, IConfiguration configuration)
    {
        _trinsicService = trinsicService;
        _configuration = configuration;
    }

    public async Task<CreateProofResponse>  CreateProof(string universityTemplateId,
        string nonce, string userAuthToken)
    {
        // Auth token from user 
        _trinsicService.Options.AuthToken = userAuthToken;

        var createProofResponse = await _trinsicService.Credential.CreateProofAsync(new CreateProofRequest
        {
            VerificationTemplateId = universityTemplateId,
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

    public async Task<VerifyProofResponse> Verify(CreateProofResponse createProofResponse, string nonce)
    {
        // Verifiers auth token
        // Auth token from trinsic.id root API KEY provider
        _trinsicService.Options.AuthToken = _configuration["TrinsicCompanyXHumanResourcesOptions:ApiKey"];

        //Nonce = ByteString.CopyFrom(nonce, Encoding.Unicode)

        var verifyProofResponse = await _trinsicService.Credential.VerifyProofAsync(new VerifyProofRequest
        {
            ProofDocumentJson = createProofResponse.ProofDocumentJson, 
        });

        return verifyProofResponse;
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
