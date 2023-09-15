using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Trinsic;
using Trinsic.Services.VerifiableCredentials.Templates.V1;

namespace University.Pages;

/// <summary>
/// Create Credential Template for a new universaty diploma
/// https://docs.trinsic.id/reference/services/template-service/
/// </summary>
public class CreateNewDiplomaModel : PageModel
{
    private readonly TrinsicService _trinsicService;

    public CreateNewDiplomaModel(TrinsicService trinsicService)
    {
        _trinsicService = trinsicService;
    }

    public void OnGet()
    {
    }

    public async Task OnPostAsync()
    {
        CreateCredentialTemplateRequest createRequest = new()
        {
            Name = "An Example Credential",
            Title = "Example Credential",
            Description = "A credential for Trinsic's SDK samples",
            AllowAdditionalFields = false,
            Fields =
            {
                { "firstName", new() { Title = "First Name", Description = "Given name of holder" } },
                { "lastName", new() { Title = "Last Name", Description = "Surname of holder", Optional = true } },
                { "age", new() { Title = "Age", Description = "Age in years of holder", Type = FieldType.Number } }
            },
                    FieldOrdering =
            {
                { "firstName", new() { Order = 0, Section = "Name" } },
                { "lastName", new() { Order = 1, Section = "Name" } },
                { "age", new() { Order = 2, Section = "Miscellanous" } }
            },
            AppleWalletOptions = new()
            {
                PrimaryField = "firstName",
                SecondaryFields = { "lastName" },
                AuxiliaryFields = { "age" }
            }
        };

        var template = await _trinsicService.Template
            .CreateAsync(createRequest);
    }
}
