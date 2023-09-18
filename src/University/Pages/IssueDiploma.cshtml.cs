using System.Globalization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Trinsic;
using Trinsic.Services.UniversalWallet.V1;
using Trinsic.Services.VerifiableCredentials.Templates.V1;
using Trinsic.Services.VerifiableCredentials.V1;

namespace University.Pages;

/// <summary>
/// Create Credential Template for a new universaty diploma
/// https://docs.trinsic.id/reference/services/template-service/
/// https://docs.trinsic.id/reference/services/credential-service/
/// https://docs.trinsic.id/guide/issuance/
/// </summary>
public class IssueDiplomaModel : PageModel
{
    private readonly TrinsicService _trinsicService;
    private readonly IConfiguration _configuration;

    public string CredentialOfferUrl { get; set; } = string.Empty;

    public IssueDiplomaModel(TrinsicService trinsicService, IConfiguration configuration)
    {
        _trinsicService = trinsicService;
        _configuration = configuration;
    }

    public void Get()
    {
    }

    public async Task OnPostAsync()
    {
        // TODO get id from a database

        // Does OIDC even work with Trinsic?
        // Wallets from other providers do not work
        // Trinsic wallet does not work
        // Platform documentation do not match the APIs
        // No clear docs how to implement this basic flow using OIDC
        // Weak user authentication

        // Get template to validate that it exists
        var templateResponse = await _trinsicService.Template
            .GetAsync(new GetCredentialTemplateRequest
            {
                Id = "urn:template:peaceful-booth-zrpufxfp6l3c:diploma-credential-for-swiss-self-sovereign-identity-ssi"
            });

        var diploma = new Diploma
        {
            FirstName = "Damien",
            LastName = "Bod",
            DateOfBirth = "1998-05-23",
            DiplomaTitle = "Swiss SSI FH",
            DiplomaSpecialisation = "governance",
            DiplomaIssuedDate = DateTime.UtcNow.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
        };

        // University issuer
        _trinsicService.Options.AuthToken = _configuration["TrinsicOptions:IssuerAuthToken"];

        var response = await _trinsicService.Credential.CreateCredentialOfferAsync(
            new CreateCredentialOfferRequest
            {
                TemplateId = templateResponse.Template.Id,
                ValuesJson = JsonSerializer.Serialize(diploma),
                GenerateShareUrl = true 
            });

        CredentialOfferUrl = response.ShareUrl;

        //var credentialJson = await _trinsicService.Credential
        //    .IssueFromTemplateAsync(new IssueFromTemplateRequest
        //    {
        //        TemplateId = templateResponse.Template.Id,
        //        ValuesJson = JsonSerializer.Serialize(diploma),
        //    });

        //Console.WriteLine(credentialJson.ToString());
    }
}
