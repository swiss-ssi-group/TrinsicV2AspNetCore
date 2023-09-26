using Microsoft.AspNetCore.Mvc.Rendering;
using Trinsic;
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
    public readonly List<SelectListItem> TrustedUniversities = new List<SelectListItem>
    {
        new SelectListItem { Text ="University SSI Schweiz", Value= "https://schema.trinsic.cloud/peaceful-booth-zrpufxfp6l3c/diploma-credential-for-swiss-self-sovereign-identity-ssi"}
    };

    public DiplomaVerifyService(TrinsicService trinsicService, IConfiguration configuration)
    {
        _trinsicService = trinsicService;
        _configuration = configuration;
    }

    public async Task<VerifyProofResponse> Verify(string studentProof, string universityCredentialScheme)
    {
        // Verifiers auth token
        // Auth token from trinsic.id root API KEY provider
        _trinsicService.Options.AuthToken = _configuration["TrinsicCompanyXHumanResourcesOptions:ApiKey"];

        var verifyProofResponse = await _trinsicService.Credential.VerifyProofAsync(new VerifyProofRequest
        {
            ProofDocumentJson = studentProof,
        });

        // TODO verify
        // credentialSchema:id == DiplomaCredentialForSwissSelfSovereignIdentitySSI, i.e value from TrustedUniversities
        return verifyProofResponse;
    }
}
