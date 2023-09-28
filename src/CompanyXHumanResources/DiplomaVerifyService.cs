using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
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
    public List<SelectListItem> TrustedUniversities = new();
    public List<SelectListItem> TrustedCredentials = new();

    public DiplomaVerifyService(TrinsicService trinsicService, IConfiguration configuration)
    {
        _trinsicService = trinsicService;
        _configuration = configuration;

        TrustedUniversities = _configuration.GetSection("TrustedUniversities")!.Get<List<SelectListItem>>()!;
        TrustedCredentials = _configuration.GetSection("TrustedCredentials")!.Get<List<SelectListItem>>()!;
    }

    public async Task<(VerifyProofResponse? Proof, bool IsValid)> Verify(string studentProof, string universityIssuer)
    {
        // Verifiers auth token
        // Auth token from trinsic.id root API KEY provider
        _trinsicService.Options.AuthToken = _configuration["TrinsicCompanyXHumanResourcesOptions:ApiKey"];

        var verifyProofResponse = await _trinsicService.Credential.VerifyProofAsync(new VerifyProofRequest
        {
            ProofDocumentJson = studentProof,
        });

        var jsonObject = JsonNode.Parse(studentProof)!;
        var credentialSchemaId = jsonObject["credentialSchema"]!["id"];
        var issuer = jsonObject["issuer"];

        // check issuer
        if (universityIssuer != issuer!.ToString())
        {
            return (null, false);
        }

        // Do we need to check if the SchemeUri is once we trust
        //if (universityCredentialScheme != credentialSchemaId!.ToString())
        //{
        //    return (null, false);
        //}

        return (verifyProofResponse, true);
    }
}
