using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Trinsic;
using Trinsic.Services.VerifiableCredentials.Templates.V1;

namespace University.Pages;

/// <summary>
/// Create Credential Template for a new universaty diploma
/// https://docs.trinsic.id/reference/services/template-service/
/// https://docs.trinsic.id/reference/services/credential-service/
/// </summary>
public class IssueDiplomaModel : PageModel
{
    private readonly TrinsicService _trinsicService;

    public IssueDiplomaModel(TrinsicService trinsicService)
    {
        _trinsicService = trinsicService;
    }

    public void Get()
    {    
    }

    public async Task OnPostAsync()
    {
        // TODO get id from a database

        // Get template to validate that it exists
        var templateResponse = await _trinsicService.Template
            .GetAsync(new GetCredentialTemplateRequest
            {
                Id = "urn:template:peaceful-booth-zrpufxfp6l3c:diploma-credential-for-swiss-self-sovereign-identity-ssi"
            });

        //var credentialJson = await _trinsicService.Credential
        //    .IssueFromTemplateAsync(new()
        //    {
        //        TemplateId = templateResponse.Template.Id,
        //        ValuesJson = values
        //    });
    }
}
