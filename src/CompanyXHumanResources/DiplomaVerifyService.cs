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

    public async Task<CreateProofResponse>  Verify(string universityTemplateId)
    {
        // Auth token from trinsic.id root API KEY provider
        _trinsicService.Options.AuthToken = _configuration["TrinsicCompanyXHumanResourcesOptions:ApiKey"];

        var proof = await _trinsicService.Credential.CreateProofAsync(new CreateProofRequest
        {
            VerificationTemplateId = universityTemplateId
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


        return proof;
    }


  


}
