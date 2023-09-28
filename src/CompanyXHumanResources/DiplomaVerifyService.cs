using System.Text.Json.Nodes;
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
        new SelectListItem { Text ="University SSI Schweiz SSI", Value="did:web:peaceful-booth-zrpufxfp6l3c.connect.trinsic.cloud:zV9t25XybyBV7qEB1v6u9Bb"}
    };

    public readonly List<SelectListItem> TrustedCredentials = new List<SelectListItem>
    {
        new SelectListItem { Text ="University SSI Schweiz SSI Diploma", Value= "https://schema.trinsic.cloud/peaceful-booth-zrpufxfp6l3c/diploma-credential-for-swiss-self-sovereign-identity-ssi"},
        new SelectListItem { Text ="FH Basel UX Engineer Diploma", Value= "https://schema.trinsic.cloud/peaceful-booth-zrpufxfp6l3c/fh-basel-ux-engineer"}
    };

    public DiplomaVerifyService(TrinsicService trinsicService, IConfiguration configuration)
    {
        _trinsicService = trinsicService;
        _configuration = configuration;
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
