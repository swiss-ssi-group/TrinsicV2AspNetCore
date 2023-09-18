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
    private readonly IConfiguration _configuration;

    public CreateNewDiplomaModel(TrinsicService trinsicService, IConfiguration configuration)
    {
        _trinsicService = trinsicService;
        _configuration = configuration;
    }

    public void OnGet()
    {
    }

    public async Task OnPostAsync()
    {
        CreateCredentialTemplateRequest createRequest = new()
        {
            Name = "Diploma Credential for Swiss Self Sovereign Identity SSI",
            Title = "Diploma SSI degree",
            Description = "A University Diploma Credential for Swiss Self Sovereign Identity SSI",
            AllowAdditionalFields = false,
            Fields =
            {
                { "firstName", new() { Title = "First Name", Description = "first name" } },
                { "lastName", new() { Title = "Last Name", Description = "Surname" } },
                { "dateOfBirth", new() { Title = "Date of Birth", Description = "Date of birth" } },
                { "diplomaTitle", new() { Title = "Title of Diploma", Description = "Title of Diploma" } },
                { "diplomaSpecialisation", new() { Title = "Specialisation", Description = "Specialisation", Optional=true } },
                { "diplomaIssuedDate", new() { Title = "Diploma issued date", Description = "date when diploma was issued, or awrded" } },
                
            },
            FieldOrdering =
            {
                { "firstName", new() { Order = 0, Section = "Name" } },
                { "lastName", new() { Order = 1, Section = "Name" } },
                { "dateOfBirth", new() { Order = 2, Section = "Name" } },
                { "diplomaTitle", new() { Order = 3, Section = "Diploma" } },
                { "diplomaSpecialisation", new() { Order = 4, Section = "Diploma" } },
                { "diplomaIssuedDate", new() { Order = 5, Section = "Diploma" } }
            }, 
            AppleWalletOptions = new()
            {
                PrimaryField = "firstName",
                SecondaryFields = { "lastName", "dateOfBirth"},
                AuxiliaryFields = {  "diplomaTitle", "diplomaIssuedDate" }
            }
        };

        _trinsicService.Options.AuthToken = _configuration["TrinsicOptions:IssuerAuthToken"];

        // TODO save id in a database for later usage
        var template = await _trinsicService.Template
            .CreateAsync(createRequest);
    }
}
