using System.Text.Json;
using Trinsic;
using Trinsic.Services.TrustRegistry.V1;
using Trinsic.Services.VerifiableCredentials.Templates.V1;
using Trinsic.Services.VerifiableCredentials.V1;

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

    public async Task<CreateCredentialOfferResponse?> IssuerStudentDiplomaCredentialOffer(Diploma diploma)
    {

        var templateResponse = await GetUniversityDiplomaTemplate(
            GetUniversityDiplomaTemplateId());

        // University issuer
        _trinsicService.Options.AuthToken = _configuration["TrinsicOptions:IssuerAuthToken"];

        var response = await _trinsicService.Credential.CreateCredentialOfferAsync(
            new CreateCredentialOfferRequest
            {
                TemplateId = templateResponse.Template.Id,
                ValuesJson = JsonSerializer.Serialize(diploma),
                GenerateShareUrl = true
            });

        return response;
    }

    public async Task<CreateCredentialTemplateResponse> CreateDiplomaTemplate(CreateCredentialTemplateRequest diplomaTemplate)
    {
        _trinsicService.Options.AuthToken = _configuration["TrinsicOptions:IssuerAuthToken"];

        // TODO save id in a database for later usage
        var template = await _trinsicService.Template
            .CreateAsync(diplomaTemplate);

        return template;
    }
}
