using Microsoft.Extensions.Configuration;
using Trinsic;
using Trinsic.Services.TrustRegistry.V1;
using Trinsic.Services.VerifiableCredentials.Templates.V1;

namespace University;

public class UniversityServices
{
    private readonly TrinsicService _trinsicService;
    private readonly IConfiguration _configuration;
    private readonly string _universityTemplateId = "urn:template:peaceful-booth-zrpufxfp6l3c:diploma-credential-for-swiss-self-sovereign-identity-ssi";

    public UniversityServices(TrinsicService trinsicService, IConfiguration configuration)
    {
        _trinsicService = trinsicService;
        _configuration = configuration;
    }

    public string GetUniversityDiplomaTemplateId()
    {
        // Use a DB here
        return _universityTemplateId;
    }

    public async Task<GetCredentialTemplateResponse>  GetUniversityDiplomaTemplate(string universityTemplateId)
    {
        // original API 
        _trinsicService.Options.AuthToken = _configuration["TrinsicOptions:ApiKey"];

        var templateResponse = await _trinsicService.Template
            .GetAsync(new GetCredentialTemplateRequest
            {
                Id = universityTemplateId
            });

        return templateResponse;
    }

    public async Task<RegisterMemberResponse> RegisterIssuer(string walletId, string schemaUri)
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
